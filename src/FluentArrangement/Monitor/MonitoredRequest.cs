namespace FluentArrangement
{
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
}