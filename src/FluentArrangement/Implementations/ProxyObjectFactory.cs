using System;
using Castle.DynamicProxy;

namespace FluentArrangement
{
    public class ProxyObjectFactory : IFactory
    {
        private bool CanCreateObject(Type type)
        {
            return type.IsInterface;
        }

        private ProxyGenerator _proxyGenerator = new ProxyGenerator();

        public ICreateResponse Create(ICreateRequest request, IScope scope)
        {
            if(!CanCreateObject(request.Type))
                return new NotCreatedResponse();

            var interceptor = new ScopeInterceptor(scope);
            var createdObject = _proxyGenerator.CreateInterfaceProxyWithoutTarget(request.Type, interceptor);

            return new CreatedObjectResponse(createdObject);
        }
    }
}