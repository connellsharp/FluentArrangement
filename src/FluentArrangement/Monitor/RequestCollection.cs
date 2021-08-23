using System.Collections;
using System.Collections.Generic;

namespace FluentArrangement
{
    public class RequestCollection : IEnumerable<MonitoredRequest>
    {
        private readonly IList<MonitoredRequest> _list = new List<MonitoredRequest>();
        private readonly RequestCollection? _parentCollection;

        public RequestCollection(RequestCollection? parentCollection)
        {
            _parentCollection = parentCollection;
        }

        internal void Log(ICreateRequest request, ICreateResponse response)
        {
            _list.Add(new MonitoredRequest(request, response));

            _parentCollection?.Log(request, response);
        }

        public IEnumerator<MonitoredRequest> GetEnumerator() => _list.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}