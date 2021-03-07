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

        public IEnumerable<MonitorCall> AllCalls => Interceptor.AllCalls;

        public IEnumerable<MonitorCall> CallsTo(string methodName)
            => AllCalls.Where(c => c.Method.Name == methodName);
    }
}