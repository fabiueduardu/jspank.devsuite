using System;

namespace JSpank.DevSuite.Domain.Abstraction
{
    public interface ILogger
    {
        void Write(string message);

        void Write(Exception exception);
    }

    public class Logger : ILogger
    {
        public void Write(Exception value)
        {
            Console.WriteLine("Exception: {0}", value.Message);
        }

        public void Write(string value)
        {
            Console.WriteLine("Message: {0}", value);
        }
    }
}
