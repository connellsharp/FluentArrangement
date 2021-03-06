using System;

namespace FluentArrangement
{
    internal class ScopeServiceProvider : IServiceProvider
    {
        private readonly IScope _scope;

        public ScopeServiceProvider(IScope scope)
            => _scope = scope;

        public object GetService(Type serviceType)
            => _scope.CreateObjectFromType(serviceType);
    }
}