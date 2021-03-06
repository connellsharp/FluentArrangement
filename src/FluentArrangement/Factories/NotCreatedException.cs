using System;
using System.Runtime.Serialization;

namespace FluentArrangement
{
    [Serializable]
    public class NotCreatedException : Exception
    {
        internal NotCreatedException(string message)
            : base(message + Environment.NewLine + "Try adding an IFactory that can handle this request.")
        {
        }

        internal NotCreatedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected NotCreatedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}