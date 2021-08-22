using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FluentArrangement
{
    public record MonitoredMethodCall
    {
        public MethodInfo Method { get; init; } = null!;

        public IEnumerable<object> Arguments { get; init; } = null!;

        public IEnumerable<KeyValuePair<ParameterInfo, object>> ZipArguments()
            => Method.GetParameters()
                     .Zip(Arguments, (p, a) => new KeyValuePair<ParameterInfo, object>(p, a));

        public object? ReturnValue { get; init; } = null!;
    }
}