using System;
using System.Collections.Generic;
using PayloadKeys = AopLogging.Constants.PayloadKeys;

namespace AopLogging
{
    public class LogEntryGenerator : ILogEntryGenerator
    {
        public LogEntry CreateInvocationLogEntry(string className, string methodName, object[] args)
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

        public LogEntry CreateLeavingLogEntry(string className, string methodName, object[] args, Type returnType,
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

        public LogEntry CreateExceptionLogEntry(string className, string methodName, object[] args, Exception innerException)
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