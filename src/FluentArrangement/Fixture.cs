using System;

namespace FluentArrangement
{
    public class Fixture : IFixture
    {
        private AggregateFactory Factory = new AggregateFactory();

        public IFixture Register(IFactory factory)
        {
            Factory.Add(factory);
            return this;
        }

        public T Create<T>(string name = null)
        {
            var response = Factory.Create(new CreateTypeRequest(typeof(T)));

            if(response.HasCreated)
                return (T)response.CreatedObject;

            throw new NoFactoryFoundException();
        }
    }

    public interface IFixture
    {
        IFixture Register(IFactory factory);

        T Create<T>(string name = null);
    }

    public static class FixtureExtensions
    {
        public static IFixture RegisterType<T>(this IFixture fixture, T instance)
            => fixture.RegisterType<T>(() => instance);

        public static IFixture RegisterType<T>(this IFixture fixture, Func<T> func)
            => fixture.Register(new TypeFactory<T>(func));

        // public static IFixture RegisterParameter<T>(this IFixture fixture, string name, T value)
        //     => fixture.Register(new ParameterFactory<T>(name, value));
    }
}