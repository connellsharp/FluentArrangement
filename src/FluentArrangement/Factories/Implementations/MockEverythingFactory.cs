using System;
using Castle.DynamicProxy;

namespace FluentArrangement
{
    public class MockEverythingFactory : IFactory
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

            var interceptor = new MyInterceptor(scope);
            var createdObject = _proxyGenerator.CreateInterfaceProxyWithoutTarget(request.Type, interceptor);

            return new CreatedObjectResponse(createdObject);
        }

        private class MyInterceptor : IInterceptor
        {
            private IScope _scope;

            public MyInterceptor(IScope scope)
            {
                _scope = scope;
            }

            public void Intercept(IInvocation invocation)
            {
                var value = _scope.Create(new CreateTypeRequest(invocation.Method.ReturnType));
                invocation.ReturnValue = value.CreatedObject;
            }
        }
    }
}