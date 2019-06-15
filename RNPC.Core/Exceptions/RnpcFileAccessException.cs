using System;

namespace RNPC.Core.Exceptions
{
    public class RnpcFileAccessException : Exception
    {
        public RnpcFileAccessException(string message, Exception inner): base(message, inner)
        {
        }
    }
}
