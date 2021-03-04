using System;
using System.Runtime.Serialization;

namespace FluentArrangement
{
    [Serializable]
    public class NoFactoryFoundException : Exception
    {
        internal NoFactoryFoundException()
            : this("No factory was found to create the type.")
        {
        }

        internal NoFactoryFoundException(string message)
            : base(message)
        {
        }

        internal NoFactoryFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected NoFactoryFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}