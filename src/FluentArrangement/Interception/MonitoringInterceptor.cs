using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;

namespace FluentArrangement
{
    internal class MonitoringInterceptor : IInterceptor
    {
        private IList<MonitorCall> _allCalls = new List<MonitorCall>();
        public IEnumerable<MonitorCall> AllCalls => _allCalls;

        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();
            
            _allCalls.Add(new MonitorCall
            {
                Method = invocation.Method,
                Arguments = invocation.Method.GetParameters()
                            .Zip(invocation.Arguments, (p, a) => new KeyValuePair<ParameterInfo, object>(p, a)),
                ReturnValue = invocation.ReturnValue
            });
        }
    }
}