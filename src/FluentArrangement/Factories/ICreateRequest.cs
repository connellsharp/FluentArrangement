using System;
using System.Reflection;

namespace FluentArrangement
{
    public interface ICreateRequest
    {
        Type Type { get; }

        NotCreatedException GetNotCreatedException();
    }

    public class CreateTypeRequest : ICreateRequest
    {
        public CreateTypeRequest(Type type)
        {
            Type = type;
        }

        public Type Type { get; }

        public NotCreatedException GetNotCreatedException()
            => new NotCreatedException($"Cannot create type {Type.Name}.");
    }

    public class CreatePropertyRequest : ICreateRequest
    {
        public CreatePropertyRequest(PropertyInfo property)
        {
            Property = property;
        }

        public PropertyInfo Property { get; }

        public Type Type => Property.PropertyType;

        public NotCreatedException GetNotCreatedException()
            => new NotCreatedException($"Cannot set property {Property.DeclaringType.Name}.{Property.Name} of type {Property.PropertyType.Name}.");
    }

    public class CreateParameterRequest : ICreateRequest
    {
        public CreateParameterRequest(ParameterInfo parameter)
        {
            Parameter = parameter;
        }

        public ParameterInfo Parameter { get; }

        public Type Type => Parameter.ParameterType;

        public NotCreatedException GetNotCreatedException() 
            => new NotCreatedException($"Cannot set parameter '{Parameter.Name}' of type {Parameter.ParameterType.Name}.");
    }

    public class CreateReturnValueRequest : ICreateRequest
    {
        public CreateReturnValueRequest(MethodInfo method)
        {
            Method = method;
        }

        public MethodInfo Method { get; }

        public Type Type => Method.ReturnType;

        public NotCreatedException GetNotCreatedException()
            => new NotCreatedException($"Cannot create return value for method '{Method.DeclaringType.Name}.{Method.Name}' of type {Method.ReturnType.Name}.");
    }
}