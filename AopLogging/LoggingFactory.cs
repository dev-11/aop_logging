namespace AopLogging
{
    public static class LoggingFactory
    {
        public static IDummyObject DummyObjectWithLogging => LoggingProxy<IDummyObject>.Create(new DummyObject(), new Logger(), new LogEntryGenerator(new ArgsGenerator()));
        public static ILogger LoggerWithLogging => LoggingProxy<ILogger>.Create(new Logger(), new Logger(),new LogEntryGenerator(new ArgsGenerator()));
    }
}