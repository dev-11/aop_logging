using System;
using System.Collections.Generic;
using System.Reflection;

namespace AopLogging
{
    public class LoggingProxy<T> : DispatchProxy
    {
        private T _decorated;
        private ILogger _logger;
        private IArgsGenerator _argsGenerator;

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            try
            {
                _logger.Log(new LogObject
                {
                    LogLevel = LogLevel.Information,
                    LogType = LogType.Invoke,
                    Payload = new Dictionary<string, string>
                    {
                        {"FullName", _decorated.GetType().FullName},
                        {"Method", targetMethod.Name},
                        {"Args", string.Join(", ", args)}
                    }
                });

                var result = targetMethod.Invoke(_decorated, args);

                _logger.Log(new LogObject
                {
                    LogLevel = LogLevel.Information,
                    LogType = LogType.Leave,
                    Payload = new Dictionary<string, string>
                    {
                        {"FullName", _decorated.GetType().FullName},
                        {"Method", targetMethod.Name},
                        {"Args", _argsGenerator.ToFlatString(args)},
                        {"Return type", targetMethod.ReturnType.ToString()},
                        {"Return value", result?.ToString()}
                    }
                });
                
                return result;
            }
            catch (Exception ex) when (ex is TargetInvocationException)
            {
                var innerException = ex.InnerException ?? ex; 
                
                _logger.Log(new LogObject
                {
                    LogLevel = LogLevel.Error,
                    LogType = LogType.Exception,
                    Payload = new Dictionary<string, string>
                    {
                        {"FullName", _decorated.GetType().FullName},
                        {"Method", targetMethod.Name},
                        {"Args", _argsGenerator.ToFlatString(args)},
                        {"Exception", innerException.ToString()}
                    }
                });
                
                throw innerException;
            }
        }

        public static T Create(T decorated, ILogger logger, IArgsGenerator argsGenerator)
        {
            object proxy = Create<T, LoggingProxy<T>>();
            ((LoggingProxy<T>)proxy).SetParameters(decorated, logger, argsGenerator);

            return (T)proxy;
        }

        private void SetParameters(T decorated, ILogger logger, IArgsGenerator argsGenerator)
        {
            if (decorated == null)
            {
                throw new ArgumentNullException(nameof(decorated));
            }
            
            _decorated = decorated;

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _argsGenerator = argsGenerator ?? throw new ArgumentNullException(nameof(argsGenerator));
        }
    }
}