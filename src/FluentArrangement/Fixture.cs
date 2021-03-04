using System;

namespace FluentArrangement
{
    public class Fixture : IFixture
    {
        private FactoriesScope _topLevelScope = new FactoriesScope();

        public IFixture Register(IFactory factory)
        {
            _topLevelScope.AddFactory(factory);
            return this;
        }

        public T Create<T>(string name = null)
        {
            return _topLevelScope
                .Create(new CreateTypeRequest(typeof(T), _topLevelScope))
                .GetRequiredCreatedObject<T>();
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