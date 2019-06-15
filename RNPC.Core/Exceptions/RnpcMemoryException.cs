using System;

namespace RNPC.Core.Exceptions
{
    public class RnpcMemoryException : Exception
    {
        public RnpcMemoryException(string message) : base(message) { }
    }
}