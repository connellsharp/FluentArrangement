using System;

namespace FluentArrangement
{
    public static class FixtureExtensions
    {
        public static IFixture Use(this IFixture fixture, IFactory factory)
        {
            fixture.Register(factory);
            return fixture;
        }

        public static IFixture Use<T>(this IFixture fixture, Func<IScope, T> func)
            => fixture.Use(new FuncFactory<T>(func));

        public static IFixture Use<T>(this IFixture fixture, Func<T> func)
            => fixture.Use(_ => func());

        public static IFixture UseInstance<T>(this IFixture fixture, T instance)
            => fixture.Use<T>(() => instance);

        public static IFixture UseType<TImplementation, TAbstraction>(this IFixture fixture)
            where TImplementation : TAbstraction
            => fixture.Use<TAbstraction>(scope => scope.Create<TImplementation>());

        // public static IFixture UseParameter<T>(this IFixture fixture, string name, T value)
        //     => fixture.Use(new ParameterFactory<T>(name, value));

        public static IFixture UseModels(this IFixture fixture)
            => fixture.Use(new CtorAndPropsFactory());

        public static IFixture UseInterfaceProxies(this IFixture fixture)
            => fixture.Use(new MockEverythingFactory());
    }
}