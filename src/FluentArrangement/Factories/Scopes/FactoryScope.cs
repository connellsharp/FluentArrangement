namespace FluentArrangement
{
    /// <summary>
    /// A scope that creates objects from a factory and falls back to a parent scope.
    /// </summary>
    internal class FactoryScope : IScope
    {
        private readonly IFactory _factory;
        private readonly IScope _parentScope;

        public FactoryScope(IFactory factory, IScope parentScope)
        {
            _factory = factory;
            _parentScope = parentScope;
        }

        public object? CreateObject(ICreateRequest request, IScope creationScope)
        {
            var response = _factory.Create(request, creationScope);

            if(response.HasCreated)
                return response.CreatedObject;

            return _parentScope.CreateObject(request, creationScope);
        }
    }
}