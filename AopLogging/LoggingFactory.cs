namespace AopLogging
{
    public class LoggingFactory
    {
        public static ILogger LoggerWithLogging => LoggingProxy<ILogger>.Create(new Logger(), new Logger(),new LogEntryGenerator());
    }
}