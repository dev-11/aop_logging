using System;

namespace AopLogging
{
    public class Logger : ILogger
    {
        public void Log(LogEntry logEntry)
        {
            Console.WriteLine($@"{logEntry?.LogLevel}|{logEntry?.LogType}|{logEntry?.Payload?.FlattenDictionary()}");
        }
    }
}