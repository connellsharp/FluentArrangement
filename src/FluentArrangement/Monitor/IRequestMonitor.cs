using System.Collections.Generic;

namespace FluentArrangement
{
    public interface IRequestMonitor
    {
        IEnumerable<MonitoredRequest> Requests { get; }

        void Log(ICreateRequest request, ICreateResponse response);
    }
}