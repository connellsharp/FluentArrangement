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
            var request = new CreateReturnValueRequest(invocation.Method, invocation.Arguments);
            var createdObject = _scope.CreateObject(request);
            invocation.ReturnValue = createdObject;
        }
    }
}