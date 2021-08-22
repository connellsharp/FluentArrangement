using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;

namespace FluentArrangement
{
    internal class MonitoringInterceptor : IInterceptor
    {
        private IList<MonitoredMethodCall> _allCalls = new List<MonitoredMethodCall>();
        public IEnumerable<MonitoredMethodCall> AllCalls => _allCalls;

        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();
            
            _allCalls.Add(new MonitoredMethodCall
            {
                Method = invocation.Method,
                Arguments = invocation.Arguments,
                ReturnValue = invocation.ReturnValue
            });
        }
    }
}