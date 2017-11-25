using System;

namespace DAO
{
    public class DaoException : Exception
    {
        public DaoException(string message) : base(message) {}
    }
}