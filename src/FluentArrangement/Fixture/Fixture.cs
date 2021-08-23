namespace FluentArrangement
{
    public class Fixture : ScopeFixture
    {
        public Fixture()
            : base(new RootScope(), new RequestMonitor())
        {
            Register(new VoidFactory());
        }
    }
}