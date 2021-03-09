using System.Collections.Generic;
using System.Reflection;

namespace FluentArrangement
{
    public record MonitorCall
    {
        public MethodInfo Method { get; init; } = null!;

        public IEnumerable<KeyValuePair<ParameterInfo, object>> Arguments { get; init; } = null!;

        public object? ReturnValue { get; init; }
    }
}