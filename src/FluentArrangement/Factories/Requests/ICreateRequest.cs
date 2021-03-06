using System;
using System.Reflection;

namespace FluentArrangement
{
    public interface ICreateRequest
    {
        Type Type { get; }
    }

    internal interface ICreateRequestWithException : ICreateRequest
    {
        NotCreatedException GetNotCreatedException();
    }

    public class CreateTypeRequest : ICreateRequestWithException
    {
        public CreateTypeRequest(Type type)
        {
            Type = type;
        }

        public Type Type { get; }

        public NotCreatedException GetNotCreatedException()
        {
            return new NotCreatedException($"Cannot create type {Type.Name}.");
        }
    }

    internal class CreatePropertyRequest : ICreateRequestWithException
    {
        public CreatePropertyRequest(PropertyInfo property)
        {
            Property = property;
        }

        public PropertyInfo Property { get; }

        public Type Type => Property.PropertyType;

        public NotCreatedException GetNotCreatedException()
        {
            return new NotCreatedException($"Cannot set property {Property.DeclaringType.Name}.{Property.Name} of type {Property.PropertyType.Name}.");
        }
    }

    internal class CreateParameterRequest : ICreateRequestWithException
    {
        public CreateParameterRequest(ParameterInfo parameter)
        {
            Parameter = parameter;
        }

        public ParameterInfo Parameter { get; }

        public Type Type => Parameter.ParameterType;

        public NotCreatedException GetNotCreatedException()
        {
            return new NotCreatedException($"Cannot set parameter '{Parameter.Name}' of type {Parameter.ParameterType.Name}.");
        }
    }

    internal class CreateReturnValueRequest : ICreateRequestWithException
    {
        public CreateReturnValueRequest(MethodInfo method)
        {
            Method = method;
        }

        public MethodInfo Method { get; }

        public Type Type => Method.ReturnType;

        public NotCreatedException GetNotCreatedException()
        {
            return new NotCreatedException($"Cannot create return value for method '{Method.DeclaringType.Name}.{Method.Name}' of type {Method.ReturnType.Name}.");
        }
    }
}