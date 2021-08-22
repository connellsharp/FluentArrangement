using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FluentArrangement
{
    public class RequestCollection
    {
        private readonly IList<MonitoredRequest> _list = new List<MonitoredRequest>();

        internal void Log(ICreateRequest request, ICreateResponse response)
        {
            _list.Add(new MonitoredRequest(request, response));
        }

        public IEnumerable<MonitoredRequest> Requests => _list.AsEnumerable();
    }

    public record MonitoredRequest
    {
        public MonitoredRequest(ICreateRequest request, ICreateResponse response)
        {
            Request = request;
            Response = response;
        }

        public ICreateRequest Request { get; }

        public ICreateResponse Response { get; }
    }

    public record MonitoredMethodCall
    {
        public MethodInfo Method { get; init; } = null!;

        public IEnumerable<KeyValuePair<ParameterInfo, object>> Arguments { get; init; } = null!;

        public object? ReturnValue { get; init; }
    }

    public static class MonitoredRequestExtensions
    {
        public static IEnumerable<MonitoredMethodCall> GetMethodCalls(this IEnumerable<MonitoredRequest> requests)
            => from request in requests
               let methodRequest = request.Request as CreateReturnValueRequest
               where methodRequest != null
               select new MonitoredMethodCall
               {
                    Method = methodRequest.Method,
                    Arguments = methodRequest.Method.GetParameters()
                                .Zip(methodRequest.Arguments, (p, a) => new KeyValuePair<ParameterInfo, object>(p, a)),
                    ReturnValue = request.Response.CreatedObject
               };
    }
}