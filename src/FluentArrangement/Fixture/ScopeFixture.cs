using System;

namespace FluentArrangement
{
    public class ScopeFixture : IFixture
    {
        private AggregateFactory _aggregateFactory;
        private FactoryScope _thisScope;
        
        internal IScope ThisScope => _thisScope;

        internal ScopeFixture(IScope parentScope)
        {
            _aggregateFactory = new AggregateFactory();
            _thisScope = new FactoryScope(_aggregateFactory, parentScope);
        }

        public void Register(IFactory factory)
        {
            _aggregateFactory.AddFactory(factory);
        }

        public object? Create(Type type)
        {
            return _thisScope.CreateObjectFromType(type);
        }

        public IFixture NewScope()
        {
            return new ScopeFixture(_thisScope);
        }
    }
}