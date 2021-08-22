using System.Collections.Generic;

namespace FluentArrangement
{
    internal class AggregateFactory : IFactory
    {
        private readonly IList<IFactory> _innerFactories = new List<IFactory>();

        public void AddFactory(IFactory factory)
        {
            _innerFactories.Insert(0, factory);
        }

        public ICreateResponse Create(ICreateRequest request, IScope creationScope)
        {
            foreach(var factory in _innerFactories)
            {
                var response = factory.Create(request, creationScope);

                if(response.HasCreated)
                    return response;
            }

            return new NotCreatedResponse();
        }
    }
}