using System;

namespace FluentArrangement
{
    internal class InnerScopeFactory : IFactory
    {
        private readonly Func<ICreateRequest, bool> _filter;
        private readonly IScope _innerScope;

        public InnerScopeFactory(Func<ICreateRequest, bool> filter, IScope innerScope)
        {
            _filter = filter;
            _innerScope = innerScope;
        }

        public ICreateResponse Create(ICreateRequest request, IScope scope)
        {
            if(scope == _innerScope)
                return new NotCreatedResponse();

            if(!_filter(request))
                return new NotCreatedResponse();

            var obj = _innerScope.CreateObject(request);

            if(obj != null)
                return new CreatedObjectResponse(obj);

            return new NotCreatedResponse();
        }
    }
}