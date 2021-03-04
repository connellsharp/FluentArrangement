using System;
using System.Reflection;

namespace FluentArrangement
{
    public interface ICreateRequest
    {
        Type Type { get; }
        
        IScope Scope { get; }
    }

    public class CreateTypeRequest : ICreateRequest
    {
        public CreateTypeRequest(Type type, IScope scope)
        {
            Type = type;
            Scope = scope;
        }

        public Type Type { get; }

        public IScope Scope { get; }
    }

    internal class CreatePropertyRequest : ICreateRequest
    {
        public CreatePropertyRequest(PropertyInfo property, IScope parentFactory)
        {
            Property = property;
            Scope = parentFactory;
        }

        public PropertyInfo Property { get; }

        public Type Type => Property.PropertyType;

        public IScope Scope { get; }
    }
}