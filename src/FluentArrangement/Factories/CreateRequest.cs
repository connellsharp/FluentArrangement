using System;

namespace FluentArrangement
{
    public class CreateRequest
    {
        internal CreateRequest()
        {
        }
    }

    public class CreateTypeRequest : CreateRequest
    {
        public CreateTypeRequest(Type type)
        {
            Type = type;
        }

        public Type Type { get; }
    }
}