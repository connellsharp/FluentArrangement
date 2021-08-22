namespace FluentArrangement
{
    internal class ChildScope : IScope
    {
        private readonly IScope _parentScope;
        private readonly IFactory _factory;

        public ChildScope(IScope parentScope, IFactory factory)
        {
            _parentScope = parentScope;
            _factory = factory;
        }

        public ICreateResponse Create(ICreateRequest request, IScope creationScope)
        {
            var response = _factory.Create(request, creationScope);

            if(response.HasCreated)
                return response;

            return _parentScope.Create(request, creationScope);
        }
    }
}