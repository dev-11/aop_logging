using System;

namespace AopLogging
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