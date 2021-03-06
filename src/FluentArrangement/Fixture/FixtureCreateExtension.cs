namespace FluentArrangement
{
    public static class FixtureCreateExtension
    {
        public static T Create<T>(this IFixture fixture)
            => (T)fixture.Create(typeof(T));
    }
}