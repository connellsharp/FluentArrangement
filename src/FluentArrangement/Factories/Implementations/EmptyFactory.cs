namespace FluentArrangement
{
    internal class EmptyFactory : IFactory
    {
        public ICreateResponse Create(ICreateRequest request)
        {
            return new NotCreatedResponse();
        }
    }
}