using System;

namespace RNPC.Core.Exceptions
{
    public class NullNodeException : Exception
    {
        public NullNodeException(string message) : base(message) { }
    }
}
