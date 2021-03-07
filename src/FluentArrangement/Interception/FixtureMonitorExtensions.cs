namespace FluentArrangement
{
    public static class FixtureMonitorExtensions
    {
        public static Monitor<T> Monitor<T>(this IFixture fixture)
            where T : class
            => fixture.Use(new MonitorFactory<T>())
                      .Create<Monitor<T>>();
    }
}