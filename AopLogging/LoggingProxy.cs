using System;
using System.Reflection;

namespace AopLogging
{
    public class LoggingProxy<T> : DispatchProxy
    {
        private T _decorated;
        private ILogger _logger;
        private ILogEntryGenerator _logEntryGenerator;

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            var className = _decorated.GetType().FullName;
            var methodName = targetMethod.Name;
            var returnType = targetMethod.ReturnType;
            
            try
            {
                _logger.Log(_logEntryGenerator.CreateInvocationLogEntry(className, methodName, args));

                var result = targetMethod.Invoke(_decorated, args);

                _logger.Log(_logEntryGenerator.CreateLeavingLogEntry(className, methodName, args, returnType, result));
                
                return result;
            }
            catch (Exception ex) when (ex is TargetInvocationException)
            {
                var innerException = ex.InnerException ?? ex; 
                
                _logger.Log(_logEntryGenerator.CreateExceptionLogEntry(className, methodName, args, innerException));
                
                throw innerException;
            }
        }

        public static T Create(T decorated, ILogger logger, ILogEntryGenerator logEntryGenerator)
        {
            object proxy = Create<T, LoggingProxy<T>>();
            ((LoggingProxy<T>)proxy).SetParameters(decorated, logger, logEntryGenerator);

            return (T)proxy;
        }

        private void SetParameters(T decorated, ILogger logger, ILogEntryGenerator logEntryGenerator)
        {
            if (decorated == null)
            {
                throw new ArgumentNullException(nameof(decorated));
            }
            
            _decorated = decorated;

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logEntryGenerator = logEntryGenerator ?? throw new ArgumentNullException(nameof(logEntryGenerator));
        }
    }
}