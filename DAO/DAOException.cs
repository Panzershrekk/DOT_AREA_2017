using System;

namespace DAO
{
    public class DaoException : Exception
    {
        public DaoException() : base() {}
        public DaoException(string message) : base(message) {}
        public DaoException(string message, System.Exception inner) : base(message, inner) {}
    }
    
    public class DaoConfigurationException : Exception
    {
        public DaoConfigurationException() : base() {}
        public DaoConfigurationException(string message) : base(message) {}
        public DaoConfigurationException(string message, System.Exception inner) : base(message, inner) {}
    }
}