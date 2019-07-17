namespace AopLogging
{
    public static class LoggingFactory
    {
        public static IDummyObject DummyObjectWithLogging => LoggingProxy<IDummyObject>.Create(new DummyObject());
    }
}