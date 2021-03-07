using Castle.DynamicProxy;

namespace FluentArrangement
{
    internal class ScopeInterceptor : IInterceptor
    {
        private IScope _scope;

        public ScopeInterceptor(IScope scope)
        {
            _scope = scope;
        }

        public void Intercept(IInvocation invocation)
        {
            var createdObject = _scope.CreateObject(new CreateReturnValueRequest(invocation.Method));
            invocation.ReturnValue = createdObject;
        }
    }
}