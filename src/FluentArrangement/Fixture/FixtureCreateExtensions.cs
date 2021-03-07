namespace FluentArrangement
{
    public static class FixtureCreateExtensions
    {
        public static T Create<T>(this IFixture fixture)
            => (T)fixture.Create(typeof(T));
    }
}