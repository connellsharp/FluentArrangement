namespace FluentArrangement
{
    public class MockEverythingFactory : IFactory
    {
        public ICreateResponse Create(ICreateRequest request)
        {
            // create mock and configure props and methods to return from fixture

            return new CreatedObjectResponse(null);
        }
    }
}