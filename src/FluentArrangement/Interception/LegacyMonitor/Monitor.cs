using System;
using System.Collections.Generic;
using System.Linq;

namespace FluentArrangement
{
    public class Monitor<T> : Monitor
    {
    }

    public class Monitor
    {
        internal MonitoringInterceptor Interceptor { get; }

        internal Monitor()
        {
            Interceptor = new MonitoringInterceptor();
        }

        public IEnumerable<MonitoredMethodCall> AllCalls => Interceptor.AllCalls;

        public IEnumerable<MonitoredMethodCall> CallsTo(string methodName)
            => AllCalls.Where(c => c.Method.Name == methodName);
    }
}