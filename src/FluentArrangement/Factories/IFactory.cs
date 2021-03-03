using System;

namespace FluentArrangement
{
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

    internal class TypeFactory<T> : IFactory
    {
        public TypeFactory(Func<T> func)
        {
            
        }

        public CreateResponse Create(CreateRequest request)
        {
            return new CreateResponse(null);
        }
    }
}