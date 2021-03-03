namespace FluentArrangement
{
    internal class CtorAndPropsFactory : IFactory
    {
        public ICreateResponse Create(CreateRequest request)
        {
            // call ctor and set properties from fixture

            return new CreatedObjectResponse(null);
        }
    }
}