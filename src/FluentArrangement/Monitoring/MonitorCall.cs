using System.Collections.Generic;
using System.Reflection;

namespace FluentArrangement
{
    public class MonitorCall
    {
        public MethodInfo Method { get; internal set; }

        public IEnumerable<KeyValuePair<ParameterInfo, object>> Arguments { get; internal set; }

        public object ReturnValue { get; internal set; }
    }
}