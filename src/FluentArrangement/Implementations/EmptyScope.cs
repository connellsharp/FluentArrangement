namespace FluentArrangement
{
    internal class EmptyScope : IScope
    {
        public ICreateResponse Create(ICreateRequest request, IScope scope)
        {
            return new NotCreatedResponse();
        }
    }
}