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

        public ICreateResponse Create(ICreateRequest request)
        {
            if(!CanCreateObject(request.Type))
                return new NotCreatedResponse();

            var createdObject = _proxyGenerator.CreateInterfaceProxyWithoutTarget(request.Type, new MyInterceptor(request.ParentFactory));

            return new CreatedObjectResponse(createdObject);
        }

        private class MyInterceptor : IInterceptor
        {
            private IFactory parentFactory;

            public MyInterceptor(IFactory parentFactory)
            {
                this.parentFactory = parentFactory;
            }

            public void Intercept(IInvocation invocation)
            {
                var value = parentFactory.Create(new CreateTypeRequest(invocation.Method.ReturnType, parentFactory));
                invocation.ReturnValue = value.CreatedObject;
            }
        }
    }
}