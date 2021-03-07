using Castle.DynamicProxy;

namespace FluentArrangement
{
    internal class MonitorFactory<T> : IFactory
        where T : class
    {
        private readonly ProxyGenerator _proxyGenerator;
        private readonly Monitor<T> _monitor;

        internal MonitorFactory(Monitor<T> monitor)
        {
            _proxyGenerator = new ProxyGenerator();
            _monitor = monitor;
        }

        public ICreateResponse Create(ICreateRequest request, IScope scope)
        {
            if (request.Type == typeof(T))
                return new CreatedObjectResponse(GenerateProxy(scope));

            return new NotCreatedResponse();
        }

        private T GenerateProxy(IScope scope)
        {
            var interceptor = new ScopeInterceptor(scope);

            var createdObject = _proxyGenerator.CreateInterfaceProxyWithoutTarget<T>(
                                _monitor.Interceptor, interceptor);
            
            return createdObject;
        }
    }
}