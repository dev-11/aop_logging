using System;
using System.Linq;

namespace AopLogging
{
    public class Logger : ILogger
    {
        public void Log(LogObject logObject)
        {
            var v = logObject.Payload.Select(kvp => kvp.Key + ": " + kvp.Value.ToString()).ToArray();
            
            Console.WriteLine($@"{logObject.LogLevel} {logObject.LogType} {string.Join(',', v)}");
        }
    }
}