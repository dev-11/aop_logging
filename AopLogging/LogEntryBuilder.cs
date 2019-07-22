using System;
using System.Collections.Generic;
using PayloadKeys = AopLogging.Constants.PayloadKeys;

namespace AopLogging
{
    public class LogEntryBuilder : ILogEntryBuilder
    {
        public LogEntry BuildInvocationLogEntry(string className, string methodName, object[] args)
        {
            return new LogEntry
            {
                LogLevel = LogLevel.Information,
                LogType = LogType.Invoke,
                Payload = new Dictionary<string, string>
                {
                    { PayloadKeys.ClassNameKey, className },
                    { PayloadKeys.MethodKey, methodName },
                    { PayloadKeys.ArgsKey, args.ToFlatString() }
                }
            };
        }

        public LogEntry BuildLeavingLogEntry(string className, string methodName, object[] args, Type returnType,
            object returnValue)
        {
            return new LogEntry
            {
                LogLevel = LogLevel.Information,
                LogType = LogType.Leave,
                Payload = new Dictionary<string, string>
                {
                    { PayloadKeys.ClassNameKey, className },
                    { PayloadKeys.MethodKey, methodName },
                    { PayloadKeys.ArgsKey, args.ToFlatString() },
                    { PayloadKeys.ReturnTypeKey, returnType.ToString() },
                    { PayloadKeys.ReturnValueKey, returnValue?.ToString() }
                }
            };
        }

        public LogEntry BuildExceptionLogEntry(string className, string methodName, object[] args, Exception innerException)
        {
            return new LogEntry
            {
                LogLevel = LogLevel.Error,
                LogType = LogType.Exception,
                Payload = new Dictionary<string, string>
                {
                    { PayloadKeys.ClassNameKey, className },
                    { PayloadKeys.MethodKey, methodName },
                    { PayloadKeys.ArgsKey, args.ToFlatString() },
                    { PayloadKeys.ExceptionKey, innerException.ToString() }
                }
            };
        }
    }
}