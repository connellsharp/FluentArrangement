using System;

namespace FluentArrangement
{
    public static class FixtureForExtensions
    {
        public static IFixture For(this IFixture fixture, Func<ICreateRequest, bool> filter, Action<IFixture> configure)
        {
            var innerFixture = fixture.NewScope();
            configure(innerFixture);

            var innerScope = ((ScopeFixture)innerFixture).ThisScope; // TODO better

            return fixture.Use(new InnerScopeFactory(filter, innerScope));
        }

        public static IFixture ForType<T>(this IFixture fixture, Action<IFixture> configure)
            => fixture.For(request => typeof(T).IsAssignableFrom(request.Type), configure);
    }
}