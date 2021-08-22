using System;
using System.Collections.Generic;
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

    public class SetPropertyRequest : ICreateRequest
    {
        public SetPropertyRequest(PropertyInfo property)
        {
            Property = property;
        }

        public PropertyInfo Property { get; }

        public Type Type => Property.PropertyType;

        public NotCreatedException GetNotCreatedException()
            => new NotCreatedException($"Cannot create a value for property {Property.DeclaringType?.Name ?? "global"}.{Property.Name} of type {Property.PropertyType.Name}.");
    }

    public class PassParameterRequest : ICreateRequest
    {
        public PassParameterRequest(ParameterInfo parameter)
        {
            Parameter = parameter;
        }

        public ParameterInfo Parameter { get; }

        public Type Type => Parameter.ParameterType;

        public NotCreatedException GetNotCreatedException() 
            => new NotCreatedException($"Cannot create a value for parameter '{Parameter.Name}' of type {Parameter.ParameterType.Name}.");
    }

    public class InvokeMethodRequest : ICreateRequest
    {
        public InvokeMethodRequest(MethodInfo method, IEnumerable<object> arguments)
        {
            Method = method;
            Arguments = arguments;
        }

        public MethodInfo Method { get; }
        
        public IEnumerable<object> Arguments { get; }

        public Type Type => Method.ReturnType;

        public NotCreatedException GetNotCreatedException()
            => new NotCreatedException($"Cannot create return value for method '{Method.DeclaringType?.Name ?? "global"}.{Method.Name}' of type {Method.ReturnType.Name}.");
    }
}