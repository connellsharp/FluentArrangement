namespace FluentArrangement
{
    public class Fixture
    {
        public T Create<T>(string name = null)
        {

        }
    }

    public interface IFactory
    {
        CreateResponse Create(CreateRequest request);
    }

    internal class AggregateFactory : IFactory
    {
        public CreateResponse Create(CreateRequest request)
        {
            
        }
    }

    internal class CtorAndPropsFactory : IFactory
    {
        public CreateResponse Create(CreateRequest request)
        {
            // call ctor and set properties from fixture

            return new CreateResponse(obj);
        }
    }

    internal class MockEverythingFactory : IFactory
    {
        public CreateResponse Create(CreateRequest request)
        {
            // create mock and configure props and methods to return from fixture

            return new CreateResponse(null);
        }
    }
}