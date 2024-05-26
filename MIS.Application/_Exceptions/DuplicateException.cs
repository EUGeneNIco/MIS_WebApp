using System;

namespace MIS.Application._Exceptions
{
    public class DuplicateException : Exception
    {
        public DuplicateException(string message)
            : base(message)
        {
        }
    }
}
