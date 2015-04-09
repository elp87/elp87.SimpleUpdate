using System;

namespace elp87.SimpleUpdate
{
    [Serializable]
    public class NullVersionTableUriException : Exception
    {
        public NullVersionTableUriException() { }
        public NullVersionTableUriException(string message) : base(message) { }
        public NullVersionTableUriException(string message, Exception inner) : base(message, inner) { }
        protected NullVersionTableUriException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
