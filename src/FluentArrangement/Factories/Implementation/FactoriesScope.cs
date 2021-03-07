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

        public ICreateResponse Create(ICreateRequest request, IScope creationScope)
        {
            foreach(var factory in _innerFactories)
            {
                var response = factory.Create(request, creationScope);

                if(response.HasCreated)
                    return response;
            }

            return _parentScope.Create(request, creationScope);
        }
    }
}