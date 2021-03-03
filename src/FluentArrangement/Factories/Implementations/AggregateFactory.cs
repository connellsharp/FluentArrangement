using System.Collections.Generic;

namespace FluentArrangement
{
    internal class AggregateFactory : IFactory
    {
        private ICollection<IFactory> _innerFactories = new List<IFactory>();

        public ICreateResponse Create(CreateRequest request)
        {
            foreach(var factory in _innerFactories)
            {
                var response = factory.Create(request);

                if(response.HasCreated)
                    return response;
            }

            return new NotCreatedResponse();
        }

        public void Add(IFactory factory)
        {
            _innerFactories.Add(factory);
        }
    }
}