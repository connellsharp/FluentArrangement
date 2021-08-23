using System;
using System.Collections.Generic;

namespace FluentArrangement
{
    public class ScopeFixture : IFixture
    {
        private readonly AggregateFactory _aggregateFactory;
        private readonly FactoryScope _thisScope;
        private readonly IRequestMonitor _monitor;

        internal IScope ThisScope => _thisScope;

        internal ScopeFixture(IScope parentScope, IRequestMonitor monitor)
        {
            _aggregateFactory = new AggregateFactory();
            _thisScope = new FactoryScope(_aggregateFactory, parentScope, monitor);
            _monitor = monitor;
        }

        public void Register(IFactory factory)
        {
            _aggregateFactory.AddFactory(factory);
        }

        public IFixture NewScope()
        {
            return new ScopeFixture(_thisScope, _monitor);
        }

        public object? Create(Type type)
        {
            return _thisScope.CreateObjectFromType(type);
        }

        public IEnumerable<MonitoredRequest> Requests => _monitor.Requests;
    }
}