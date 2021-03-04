using System;
using System.Collections.Generic;

namespace FluentArrangement
{
    internal class FactoriesScope : IScope
    {
        private ICollection<IFactory> _innerFactories = new List<IFactory>();

        public void AddFactory(IFactory factory)
        {
            _innerFactories.Add(factory);
        }

        public ICreateResponse Create(ICreateRequest request)
        {
            foreach(var factory in _innerFactories)
            {
                var response = factory.Create(new ScopedRequest(request, this));

                if(response.HasCreated)
                    return response;
            }

            return request.Scope.Create(request);
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