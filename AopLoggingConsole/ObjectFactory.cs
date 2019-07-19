using AopLogging;

namespace AopLoggingConsole
{
    public static class ObjectFactory
    {
        public static ICalculator CalculatorWithLogging => LoggingProxy<ICalculator>.Create(new Calculator(), new ConsoleLogger(), new LogEntryGenerator());
    }
}