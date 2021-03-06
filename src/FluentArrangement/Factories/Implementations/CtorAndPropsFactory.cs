using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FluentArrangement
{
    public class CtorAndPropsFactory : IFactory
    {
        public ICreateResponse Create(ICreateRequest request, IScope scope)
        {
            if (!IsDtoModelType(request.Type))
                return new NotCreatedResponse();

            var ctor = GetBestCtor(request.Type.GetConstructors());

            var args = ctor.GetParameters()
                .Select(p => CreateObjectOrDefault(p, scope))
                .ToArray();

            var instance = ctor.Invoke(args);

            Hydrate(instance, scope);

            return new CreatedObjectResponse(instance);
        }

        private static object CreateObjectOrDefault(ParameterInfo parameter, IScope scope)
        {
            var response = scope.Create(new CreateParameterRequest(parameter));

            if (!response.HasCreated)
                throw new NoFactoryFoundException($"Cannot set parameter '{parameter.Name}' of type {parameter.ParameterType.Name}.");

            return response.CreatedObject;
        }

        private static void Hydrate(object instance, IScope scope)
        {
            foreach (var property in instance.GetType().GetProperties())
            {
                if (!property.CanWrite)
                    continue;

                var response = scope.Create(new CreatePropertyRequest(property));

                if (!response.HasCreated)
                    throw new NoFactoryFoundException($"Cannot set property {property.DeclaringType.Name}.{property.Name} of type {property.PropertyType.Name}.");

                property.SetValue(instance, response.CreatedObject);
            }
        }

        private ConstructorInfo GetBestCtor(IEnumerable<ConstructorInfo> constructors)
        {
            return constructors.OrderBy(c => c.GetParameters().Length).First();
        }

        private bool IsDtoModelType(Type type)
        {
            return !type.IsAbstract && !type.IsPrimitive && type != typeof(string);
        }
    }
}