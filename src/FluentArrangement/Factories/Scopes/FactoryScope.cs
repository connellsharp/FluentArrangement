namespace FluentArrangement
{
    /// <summary>
    /// A scope that creates objects from a factory and falls back to a parent scope.
    /// </summary>
    internal class FactoryScope : IScope
    {
        private readonly IFactory _factory;
        private readonly IScope _parentScope;
        private readonly IRequestMonitor _monitor;

        public FactoryScope(IFactory factory, IScope parentScope, IRequestMonitor monitor)
        {
            _factory = factory;
            _parentScope = parentScope;
            _monitor = monitor;
        }

        public object? CreateObject(ICreateRequest request, IScope creationScope)
        {
            var response = _factory.Create(request, creationScope);

            if (!response.HasCreated)
                return _parentScope.CreateObject(request, creationScope);

            _monitor.Log(request, response);
            
            return response.CreatedObject;
        }
    }
}