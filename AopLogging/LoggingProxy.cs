using System;
using System.Collections.Generic;
using System.Reflection;

namespace AopLogging
{
    public class LoggingProxy<T> : DispatchProxy
    {
        private T _decorated;
        private ILogger _logger;

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            try
            {
                _logger.Log(new LogObject
                {
                    LogLevel = LogLevel.Information,
                    LogType = LogType.Invoke,
                    Payload = new Dictionary<string, string>()
                    {
                        {"FullName", _decorated.GetType().FullName},
                        {"Method", targetMethod.Name},
                        {"Args", string.Join(',', args)}
                    }
                });

                var result = targetMethod.Invoke(_decorated, args);

                _logger.Log(new LogObject
                {
                    LogLevel = LogLevel.Information,
                    LogType = LogType.Leave,
                    Payload = new Dictionary<string, string>()
                    {
                        {"FullName", _decorated.GetType().FullName},
                        {"Method", targetMethod.Name},
                        {"Args", string.Join(',', args)}
                    }
                });
                
                return result;
            }
            catch (Exception ex) when (ex is TargetInvocationException)
            {
                LogException(ex.InnerException ?? ex, targetMethod);
                throw ex.InnerException ?? ex;
            }
        }

        public static T Create(T decorated, ILogger logger)
        {
            object proxy = Create<T, LoggingProxy<T>>();
            ((LoggingProxy<T>)proxy).SetParameters(decorated, logger);

            return (T)proxy;
        }

        private void SetParameters(T decorated, ILogger logger)
        {
            if (decorated == null)
            {
                throw new ArgumentNullException(nameof(decorated));
            }
            
            _decorated = decorated;

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private void LogException(Exception exception, MemberInfo methodInfo = null)
        {
            Console.WriteLine($"Class {_decorated.GetType().FullName}, Method {methodInfo.Name} threw exception:\n{exception}");
        }
    }
}