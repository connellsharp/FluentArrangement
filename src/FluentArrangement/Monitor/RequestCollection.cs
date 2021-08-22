using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FluentArrangement
{
    public class RequestCollection : IEnumerable<MonitoredRequest>
    {
        private readonly IList<MonitoredRequest> _list = new List<MonitoredRequest>();

        internal void Log(ICreateRequest request, ICreateResponse response)
        {
            _list.Add(new MonitoredRequest(request, response));
        }

        public IEnumerator<MonitoredRequest> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}