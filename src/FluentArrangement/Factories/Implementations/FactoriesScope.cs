using System;
using System.Collections.Generic;

namespace FluentArrangement
{
    internal class FactoriesScope : IScope
    {
        private readonly IScope _parentScope;
        private readonly IList<IFactory> _innerFactories;

        public FactoriesScope(IScope parentScope)
        {
            _parentScope = parentScope;
            _innerFactories = new List<IFactory>();
        }

        public void AddFactory(IFactory factory)
        {
            _innerFactories.Insert(0, factory);
        }

        public ICreateResponse Create(ICreateRequest request)
        {
            foreach(var factory in _innerFactories)
            {
                var response = factory.Create(request, this);

                if(response.HasCreated)
                    return response;
            }

            return _parentScope.Create(request);
        }

        private class ScopedRequest : ICreateRequest
        {
            private readonly ICreateRequest _innerRequest;

            public ScopedRequest(ICreateRequest innerRequest, IScope scope)
            {
                _innerRequest = innerRequest;
                Scope = scope;
            }

            public Type Type => _innerRequest.Type;

            public IScope Scope { get; }
        }
    }
}