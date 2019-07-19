using System;

namespace AopLoggingConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var calculator = ObjectFactory.CalculatorWithLogging;

            // logging method invoke 
            calculator.Add(1,2);
            calculator.Divide(10, 2);
            calculator.Subtract(1,2);
            calculator.Multiply(12,2);

            // logging DivideByZeroException
            calculator.Divide(12,0);
        }
    }
}