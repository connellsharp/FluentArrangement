using System;

namespace FluentArrangement
{
    public static class FixtureUseExtensions
    {
        public static IFixture Use(this IFixture fixture, IFactory factory)
        {
            fixture.Register(factory);
            return fixture;
        }

        public static IFixture Use<T>(this IFixture fixture, Func<IServiceProvider, T> func)
            => fixture.Use(new FuncFactory<T>(scope => func(new ScopeServiceProvider(scope))));

        public static IFixture Use<T>(this IFixture fixture, Func<T> func)
            => fixture.Use(_ => func());

        public static IFixture UseInstance<T>(this IFixture fixture, T instance)
            => fixture.Use<T>(() => instance);

        public static IFixture UseType<TImplementation, TAbstraction>(this IFixture fixture)
            where TImplementation : TAbstraction
            => fixture.Use<TAbstraction>(sp => sp.GetService<TImplementation>());

        public static IFixture UseDefaults(this IFixture fixture)
            => fixture.Use(new DefaultValueFactory());

        public static IFixture UseConstructorAndSetProperties(this IFixture fixture)
            => fixture.Use(new ConstructorAndPropertiesFactory());

        public static IFixture UseProxyObjects(this IFixture fixture)
            => fixture.Use(new ProxyObjectFactory());

        public static IFixture UseRandomNumbers(this IFixture fixture)
            => fixture.Use(new RandomNumberFactory());

        public static IFixture UseRandomStrings(this IFixture fixture)
            => fixture.Use(new RandomStringFactory());

        public static IFixture UseRandomBooleans(this IFixture fixture)
            => fixture.Use(new RandomBooleanFactory());

        public static IFixture UseRandomValues(this IFixture fixture)
            => fixture.UseRandomNumbers().UseRandomBooleans().UseRandomStrings();

        public static IFixture UseCompletedTasks(this IFixture fixture)
            => fixture.Use(new CompletedTaskFactory());

        public static IFixture UseDefaultCancellationTokens(this IFixture fixture)
            => fixture.Use(new DefaultCancellationTokenFactory());

        public static IFixture UseNullables(this IFixture fixture)
            => fixture.Use(new NullableNotNullFactory());

        public static IFixture UseEnumerables(this IFixture fixture)
            => fixture.Use(new EnumerableFactory());

        public static IFixture UseAsync(this IFixture fixture)
            => fixture.UseCompletedTasks().UseDefaultCancellationTokens();

        public static IFixture UseUnderlyingTypes(this IFixture fixture)
            => fixture.UseCompletedTasks().UseNullables().UseEnumerables();
    }
}