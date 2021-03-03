namespace FluentArrangement
{
    internal class MockEverythingFactory : IFactory
    {
        public ICreateResponse Create(CreateRequest request)
        {
            // create mock and configure props and methods to return from fixture

            return new CreatedObjectResponse(null);
        }
    }
}