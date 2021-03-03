using System;
using System.Reflection;

namespace FluentArrangement
{
    public interface ICreateRequest
    {
        Type Type { get; }
        
        IFactory ParentFactory { get; }
    }

    public class CreateTypeRequest : ICreateRequest
    {
        public CreateTypeRequest(Type type, IFactory parentFactory)
        {
            Type = type;
            ParentFactory = parentFactory;
        }

        public Type Type { get; }

        public IFactory ParentFactory { get; }
    }

    internal class CreatePropertyRequest : ICreateRequest
    {
        public CreatePropertyRequest(PropertyInfo property, IFactory parentFactory)
        {
            Property = property;
            ParentFactory = parentFactory;
        }

        public PropertyInfo Property { get; }

        public Type Type => Property.PropertyType;

        public IFactory ParentFactory { get; }
    }
}