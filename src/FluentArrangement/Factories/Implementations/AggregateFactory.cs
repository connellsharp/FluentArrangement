using System;
using System.Collections.Generic;

namespace FluentArrangement
{
    internal class AggregateFactory : IFactory
    {
        private ICollection<IFactory> _innerFactories = new List<IFactory>();

        public void Add(IFactory factory)
        {
            _innerFactories.Add(factory);
        }

        public ICreateResponse Create(ICreateRequest request)
        {
            foreach(var factory in _innerFactories)
            {
                var response = factory.Create(new AggregateRequest(request, this));

                if(response.HasCreated)
                    return response;
            }

            return new NotCreatedResponse();
        }

        private class AggregateRequest : ICreateRequest
        {
            private readonly ICreateRequest _innerRequest;
            private readonly AggregateFactory _factory;

            public AggregateRequest(ICreateRequest innerRequest, AggregateFactory factory)
            {
                _innerRequest = innerRequest;
                _factory = factory;
            }

            public Type Type => _innerRequest.Type;

            public IFactory ParentFactory => _factory;
        }
    }
}