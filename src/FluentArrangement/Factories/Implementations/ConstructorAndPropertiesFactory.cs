using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FluentArrangement
{
    public class ConstructorAndPropertiesFactory : IFactory
    {
        public ICreateResponse Create(ICreateRequest request, IScope scope)
        {
            if (!IsDtoModelType(request.Type))
                return new NotCreatedResponse();

            var ctor = GetBestCtor(request.Type.GetConstructors());

            var args = ctor.GetParameters()
                .Select(p => scope.CreateObject(new CreateParameterRequest(p)))
                .ToArray();

            var instance = ctor.Invoke(args);

            Hydrate(instance, scope);

            return new CreatedObjectResponse(instance);
        }

        private static void Hydrate(object instance, IScope scope)
        {
            foreach (var property in instance.GetType().GetProperties())
            {
                if (!property.CanWrite)
                    continue;

                var createdObject = scope.CreateObject(new CreatePropertyRequest(property));

                property.SetValue(instance, createdObject);
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