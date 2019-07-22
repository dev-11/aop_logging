using System;
using System.Reflection;

namespace AopLogging
{
    public class LoggingProxy<T> : DispatchProxy
    {
        private T _decorated;
        private ILogger _logger;
        private ILogEntryBuilder _logEntryBuilder;

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            var className = _decorated.GetType().FullName;
            var methodName = targetMethod.Name;
            var returnType = targetMethod.ReturnType;
            
            try
            {
                _logger.Log(_logEntryBuilder.BuildInvocationLogEntry(className, methodName, args));

                var result = targetMethod.Invoke(_decorated, args);

                _logger.Log(_logEntryBuilder.BuildLeavingLogEntry(className, methodName, args, returnType, result));
                
                return result;
            }
            catch (Exception ex) when (ex is TargetInvocationException)
            {
                var innerException = ex.InnerException ?? ex; 
                
                _logger.Log(_logEntryBuilder.BuildExceptionLogEntry(className, methodName, args, innerException));
                
                throw innerException;
            }
        }

        public static T Create(T decorated, ILogger logger, ILogEntryBuilder logEntryBuilder)
        {
            object proxy = Create<T, LoggingProxy<T>>();
            ((LoggingProxy<T>)proxy).SetParameters(decorated, logger, logEntryBuilder);

            return (T)proxy;
        }

        private void SetParameters(T decorated, ILogger logger, ILogEntryBuilder logEntryBuilder)
        {
            if (decorated == null)
            {
                throw new ArgumentNullException(nameof(decorated));
            }
            
            _decorated = decorated;

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logEntryBuilder = logEntryBuilder ?? throw new ArgumentNullException(nameof(logEntryBuilder));
        }
    }
}