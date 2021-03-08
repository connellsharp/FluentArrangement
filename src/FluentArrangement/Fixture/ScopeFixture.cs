using System;

namespace FluentArrangement
{
    public class ScopeFixture : IFixture
    {
        private FactoriesScope _thisScope;
        
        internal IScope ThisScope => _thisScope;

        internal ScopeFixture(IScope parentScope)
        {
            _thisScope = new FactoriesScope(parentScope);
        }

        public void Register(IFactory factory)
        {
            _thisScope.AddFactory(factory);
        }

        public object Create(Type type)
        {
            return _thisScope.CreateObjectFromType(type);
        }

        public IFixture NewScope()
        {
            return new ScopeFixture(_thisScope);
        }
    }
}