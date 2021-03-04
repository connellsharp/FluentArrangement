namespace FluentArrangement
{
    internal class EmptyScope : IScope
    {
        public ICreateResponse Create(ICreateRequest request)
        {
            return new NotCreatedResponse();
        }
    }
}