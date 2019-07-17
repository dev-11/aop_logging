using System;

namespace AopLogging
{
    public class Logger : ILogger
    {
        public void Log(LogObject logObject)
        {
            Console.WriteLine($@"{logObject?.LogLevel}|{logObject?.LogType}|{logObject?.Payload?.FlattenDictionary()}");
        }
    }
}