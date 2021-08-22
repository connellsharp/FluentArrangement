namespace FluentArrangement
{
    internal class VoidFactory : IFactory
    {
        public ICreateResponse Create(ICreateRequest request, IScope scope)
        {
            if(request.Type == typeof(void))
                return new CreatedVoidResponse();

            return new NotCreatedResponse();
        }
    }
}