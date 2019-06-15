using System;

namespace RNPC.Core.Exceptions
{
    public class RnpcParameterException : Exception
    {
        public RnpcParameterException(string message, Exception inner): base(message, inner) { }
        public RnpcParameterException(string message) : base(message) { }
    }
}
