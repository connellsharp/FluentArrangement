using System;

namespace FluentArrangement
{
    internal class FuncFactory<T> : IFactory
    {
        private readonly Func<IScope, T> _func;

        public FuncFactory(Func<IScope, T> func)
        {
            _func = func;
        }

        public ICreateResponse Create(ICreateRequest request, IScope scope)
        {
            if(typeof(T).IsAssignableFrom(request.Type))
            {
                var obj = _func(scope);
                return new CreatedObjectResponse(obj);
            }

            return new NotCreatedResponse();
        }
    }
}