using System;
using System.Collections.Generic;

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
                    {"FullName", className},
                    {"Method", methodName},
                    {"Args", args.ToFlatString()}
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
                    {"FullName", className},
                    {"Method", methodName},
                    {"Args", args.ToFlatString()},
                    {"Return type", returnType.ToString()},
                    {"Return value", returnValue?.ToString()}
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
                    {"FullName", className},
                    {"Method", methodName},
                    {"Args", args.ToFlatString()},
                    {"Exception", innerException.ToString()}
                }
            };
        }
    }
}