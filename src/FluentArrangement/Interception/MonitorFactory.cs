using Castle.DynamicProxy;

namespace FluentArrangement
{
    public class MonitorFactory<T> : IFactory
        where T : class
    {
        private readonly Monitor<T> _monitor = new Monitor<T>();
        private readonly ProxyGenerator _proxyGenerator = new ProxyGenerator();

        internal MonitorFactory()
        {
        }
        
        public ICreateResponse Create(ICreateRequest request, IScope scope)
        {
            if(request.Type == typeof(Monitor<T>))
                return new CreatedObjectResponse(_monitor);

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