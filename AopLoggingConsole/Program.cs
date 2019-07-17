using AopLogging;

namespace AopLoggingConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var logDummy = LoggingFactory.DummyObjectWithLogging;

            logDummy.Hw();

        }
    }
}