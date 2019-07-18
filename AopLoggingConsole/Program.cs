using System;
using AopLogging;

namespace AopLoggingConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var calculator = ObjectFactory.CalculatorWithLogging;

            Console.WriteLine(calculator.Add(calculator.Divide(1,2), calculator.Multiply(4,calculator.Subtract(9,2))));
            
        }
    }
}