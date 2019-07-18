using System;
using AopLogging;

namespace AopLoggingConsole
{
    public class DummyObject : IDummyObject
    {
        public bool Hw(string text)
        {
            Console.WriteLine(text);
            return true;
        }
    }
}