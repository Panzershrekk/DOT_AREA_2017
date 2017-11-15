using System;

namespace Properties
{
    public class PropertiesException : Exception
    {
        public PropertiesException() : base() {}
        public PropertiesException(string message) : base(message) {}
        public PropertiesException(string message, System.Exception inner) : base(message, inner) {}
    }
}