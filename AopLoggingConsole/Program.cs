using AopLogging;

namespace AopLoggingConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var logDummy = ObjectFactory.DummyObjectWithLogging;

            logDummy.Hw("hello world!");

            var l = ObjectFactory.LoggerWithLogging;
            l.Log(null);
        }
    }
}