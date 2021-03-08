using System;
using System.Linq;

namespace FluentArrangement
{
    internal class ConstructorAndPropertiesFactory : IFactory
    {
        public ICreateResponse Create(ICreateRequest request, IScope scope)
        {
            if (!CanInstantiate(request.Type))
                return new NotCreatedResponse();

            object instance = Instantiate(request.Type, scope);
            Hydrate(instance, scope);

            return new CreatedObjectResponse(instance);
        }

        private bool CanInstantiate(Type type)
            => !type.IsAbstract
            && !type.IsPrimitive
            && type != typeof(string)
            && type.GetConstructors().Any();

        private object Instantiate(Type type, IScope scope)
        {
            var shortestCtor = type.GetConstructors()
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