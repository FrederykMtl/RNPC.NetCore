using System;

namespace RNPC.Core.Exceptions
{
    public class NodeInitializationException : Exception
    {
        public NodeInitializationException(string message) : base(message) { }
        public NodeInitializationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
