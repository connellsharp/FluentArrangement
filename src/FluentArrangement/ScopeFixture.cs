namespace FluentArrangement
{
    public class ScopeFixture : IFixture
    {
        private FactoriesScope _thisScope;

        internal ScopeFixture(IScope parentScope)
        {
            _thisScope = new FactoriesScope(parentScope);
        }

        public void Register(IFactory factory)
        {
            _thisScope.AddFactory(factory);
        }

        public T Create<T>()
        {
            return _thisScope
                .Create(new CreateTypeRequest(typeof(T)))
                .GetRequiredCreatedObject<T>();
        }

        public IFixture NewScope()
        {
            return new ScopeFixture(_thisScope);
        }
    }
}