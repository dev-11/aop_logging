using System;
using AopLogging;

namespace AopLoggingConsole
{
    public class ConsoleLogger : ILogger
    {
        public void Log(LogEntry logEntry)
        {
            Console.WriteLine($@"{DateTime.Now}|{logEntry?.LogLevel}|{logEntry?.LogType}|{logEntry?.Payload?.ToFlatString()}");
        }
    }
}