using System;

namespace FluentArrangement
{
    internal class TypeFactory<T> : IFactory
    {
        private readonly Func<T> _func;

        public TypeFactory(Func<T> func)
        {
            _func = func;
        }

        public ICreateResponse Create(ICreateRequest request, IScope scope)
        {
            if(typeof(T).IsAssignableFrom(request.Type))
            {
                var obj = _func();
                return new CreatedObjectResponse(obj);
            }

            return new NotCreatedResponse();
        }
    }
}