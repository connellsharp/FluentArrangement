using System;
using System.Linq;

namespace FluentArrangement
{
    internal class ConstructorAndPropertiesFactory : IFactory
    {
        public ICreateResponse Create(ICreateRequest request, IScope scope)
        {
            if (!IsModelType(request.Type))
                return new NotCreatedResponse();

            object instance = Instantiate(request, scope);
            Hydrate(instance, scope);

            return new CreatedObjectResponse(instance);
        }

        private bool IsModelType(Type type)
        {
            return !type.IsAbstract && !type.IsPrimitive && type != typeof(string);
        }

        private object Instantiate(ICreateRequest request, IScope scope)
        {
            var shortestCtor = request.Type.GetConstructors()
                                           .OrderBy(c => c.GetParameters().Length)
                                           .First();

            var args = shortestCtor.GetParameters()
                .Select(p => scope.CreateObject(new CreateParameterRequest(p)))
                .ToArray();

            var instance = shortestCtor.Invoke(args);
            return instance;
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
    }
}