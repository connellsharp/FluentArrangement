namespace FluentArrangement
{
    public class Fixture : ScopeFixture
    {
        public Fixture()
            : base(new FactoryScope(new VoidFactory(), new RootScope()))
        {
        }
    }
}