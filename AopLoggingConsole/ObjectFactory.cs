using AopLogging;

namespace AopLoggingConsole
{
    public class ObjectFactory : LoggingFactory
    {
        public static IDummyObject DummyObjectWithLogging => LoggingProxy<IDummyObject>.Create(new DummyObject(), new Logger(), new LogEntryGenerator());
    }
}