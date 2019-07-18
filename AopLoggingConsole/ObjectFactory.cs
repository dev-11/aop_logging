using AopLogging;

namespace AopLoggingConsole
{
    public class ObjectFactory : LoggingFactory
    {
        public static ICalculator CalculatorWithLogging => LoggingProxy<ICalculator>.Create(new Calculator(), new Logger(), new LogEntryGenerator());
    }
}