using System.Collections.Generic;

namespace AopLogging
{
    public class LogEntry
    {
        public LogLevel LogLevel { get; set; }
        public LogType LogType { get; set; }
        public IDictionary<string, string> Payload { get; set; }
    }
}