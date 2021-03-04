using System;
using System.Reflection;

namespace FluentArrangement
{
    public interface ICreateRequest
    {
        Type Type { get; }
    }

    public class CreateTypeRequest : ICreateRequest
    {
        public CreateTypeRequest(Type type)
        {
            Type = type;
        }

        public Type Type { get; }
    }

    internal class CreatePropertyRequest : ICreateRequest
    {
        public CreatePropertyRequest(PropertyInfo property)
        {
            Property = property;
        }

        public PropertyInfo Property { get; }

        public Type Type => Property.PropertyType;
    }
}