using System;

namespace FluentArrangement
{
    public interface ICreateResponse
    {
        bool HasCreated { get; }

        object CreatedObject { get; }
    }

    internal class CreatedObjectResponse : ICreateResponse
    {
        public CreatedObjectResponse(object obj)
        {
            CreatedObject = obj;
        }

        public bool HasCreated => true;

        public object CreatedObject { get; }
    }

    public class NotCreatedResponse : ICreateResponse
    {
        public bool HasCreated => false;

        public object CreatedObject => throw new InvalidOperationException("Object has not been created. Check HasCreated first.");
    }
}