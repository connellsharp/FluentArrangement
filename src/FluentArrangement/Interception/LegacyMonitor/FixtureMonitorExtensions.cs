namespace FluentArrangement
{
    public static class FixtureMonitorExtensions
    {
        public static Monitor<T> Monitor<T>(this IFixture fixture)
            where T : class
        {
            var monitor = new Monitor<T>();

            fixture.Use(new MonitorFactory<T>(monitor));

            return monitor;
        }
    }
}