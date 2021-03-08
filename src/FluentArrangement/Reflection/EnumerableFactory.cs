using System;
using System.Collections;
using System.Collections.Generic;

namespace FluentArrangement
{
    internal class EnumerableFactory : IFactory
    {
        public ICreateResponse Create(ICreateRequest request, IScope scope)
        {
            if (!request.Type.InheritsGenericInterface(typeof(IEnumerable<>)))
                return new NotCreatedResponse();

            var innerType = request.Type.GetGenericInterface(typeof(IEnumerable<>)).GenericTypeArguments[0];

            var enumerable = Activator.CreateInstance(typeof(FactoryEnumerable<>).MakeGenericType(innerType), scope);

            var createdObject = innerType;

            return new CreatedObjectResponse(createdObject);
        }

        private class FactoryEnumerable<T> : IEnumerable<T>
        {
            private readonly IScope _scope;

            public FactoryEnumerable(IScope scope)
            {
                _scope = scope;
            }

            public IEnumerator<T> GetEnumerator()
            {
                yield return _scope.CreateObjectFromType<T>();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }
    }
}