using System;

namespace AopLogging
{
    public interface ILogEntryBuilder
    {
        LogEntry BuildInvocationLogEntry(string className, string methodName, object[] args);
        LogEntry BuildLeavingLogEntry(string className, string methodName, object[] args, Type returnType, object returnValue);
        LogEntry BuildExceptionLogEntry(string className, string methodName, object[] args, Exception innerException);
    }
}