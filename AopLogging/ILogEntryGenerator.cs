using System;

namespace AopLogging
{
    public interface ILogEntryGenerator
    {
        LogEntry CreateInvocationLogEntry(string className, string methodName, object[] args);
        LogEntry CreateLeavingLogEntry(string className, string methodName, object[] args, Type returnType, object returnValue);
        LogEntry CreateExceptionLogEntry(string className, string methodName, object[] args, Exception innerException);
    }
}