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

        public ICreateResponse Create(CreateRequest request)
        {
            if(request is CreateTypeRequest createTypeRequest
                && typeof(T).IsAssignableFrom(createTypeRequest.Type))
            {
                var obj = _func();
                return new CreatedObjectResponse(obj);
            }

            return new NotCreatedResponse();
        }
    }
}