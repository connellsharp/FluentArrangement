using System.Collections.Generic;
using System.Linq;

namespace FluentArrangement
{
    public static class MonitoredMethodCallExtensions
    {
        public static IEnumerable<MonitoredMethodCall> GetMethodCalls(this IEnumerable<MonitoredRequest> requests)
            => from request in requests
               let methodRequest = request.Request as CreateReturnValueRequest
               where methodRequest != null
               select new MonitoredMethodCall
               {
                    Method = methodRequest.Method,
                    Arguments = methodRequest.Arguments,
                    ReturnValue = request.Response.CreatedObject
               };

        public static IEnumerable<MonitoredMethodCall> ForType<T>(this IEnumerable<MonitoredMethodCall> methodCalls)
            => from methodCall in methodCalls
               let type = methodCall.Method.DeclaringType
               where typeof(T).IsAssignableFrom(type)
               select methodCall;

        public static IEnumerable<MonitoredMethodCall> ToMethod(this IEnumerable<MonitoredMethodCall> methodCalls, string methodName)
            => from methodCall in methodCalls
               where methodCall.Method.Name == methodName
               select methodCall;
    }
}