using System;

namespace AopLogging
{
    public class DummyObject : IDummyObject
    {
        public void Hw()
        {
            Console.WriteLine("Hello World!");
        }
    }

    public interface IDummyObject
    {
        void Hw();
    }
}