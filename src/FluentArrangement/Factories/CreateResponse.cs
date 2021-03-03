using System;

namespace FluentArrangement
{
    public interface ICreateResponse
    {
        bool HasCreated { get; }

        object CreatedObject { get; }
    }

    public class NotCreatedResponse : ICreateResponse
    {
        public bool HasCreated => false;

        public object CreatedObject => throw new InvalidOperationException("Object has not been created. Check HasCreated first.");
    }

    internal class CreatedObjectResponse : ICreateResponse
    {
        private object _obj;

        public CreatedObjectResponse(object obj)
        {
            _obj = obj;
        }

        public bool HasCreated => true;

        public object CreatedObject => _obj;
    }
}