using System;
using System.Collections.Generic;

namespace AopLogging
{
    public class LogEntryGenerator : ILogEntryGenerator
    {
        private readonly IArgsGenerator _argsGenerator;

        public LogEntryGenerator(IArgsGenerator argsGenerator)
        {
            _argsGenerator = argsGenerator;
        }

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
                    {"Args", _argsGenerator.ToFlatString(args)}
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
                    {"Args", _argsGenerator.ToFlatString(args)},
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
                    {"Args", _argsGenerator.ToFlatString(args)},
                    {"Exception", innerException.ToString()}
                }
            };
        }
    }
}