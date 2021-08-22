namespace FluentArrangement
{
    internal class FactoryScope : IScope
    {
        private readonly IFactory _factory;

        public FactoryScope(IFactory factory)
        {
            _factory = factory;
        }

        public ICreateResponse Create(ICreateRequest request, IScope scope)
        {
            return _factory.Create(request, scope);
        }
    }
}