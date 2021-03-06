﻿using System;

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

    [Serializable]
    public class InvalidBuildNumberException : Exception
    {
        public InvalidBuildNumberException() { }
        public InvalidBuildNumberException(string message) : base(message) { }
        public InvalidBuildNumberException(string message, Exception inner) : base(message, inner) { }
        protected InvalidBuildNumberException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    [Serializable]
    public class EmptyAppNameException : Exception
    {
        public EmptyAppNameException() { }
        public EmptyAppNameException(string message) : base(message) { }
        public EmptyAppNameException(string message, Exception inner) : base(message, inner) { }
        protected EmptyAppNameException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }

    [Serializable]
    public class NoUpdConfigFileException : Exception
    {
        public NoUpdConfigFileException() { }
        public NoUpdConfigFileException(string message) : base(message) { }
        public NoUpdConfigFileException(string message, Exception inner) : base(message, inner) { }
        protected NoUpdConfigFileException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
