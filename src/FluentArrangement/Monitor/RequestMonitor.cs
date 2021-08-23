using System.Collections.Generic;

namespace FluentArrangement
{
    internal class RequestMonitor : IRequestMonitor
    {
        private IList<MonitoredRequest> _requests = new List<MonitoredRequest>();

        public IEnumerable<MonitoredRequest> Requests => _requests;

        public void Log(ICreateRequest request, ICreateResponse response)
        {
            _requests.Add(new MonitoredRequest(request, response));
        }
    }
}