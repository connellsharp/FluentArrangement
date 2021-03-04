namespace FluentArrangement
{
    public interface IFixture
    {
        IFixture Register(IFactory factory);

        T Create<T>();

        IFixture NewScope();
    }
    
    public class Fixture : ScopeFixture
    {
        public Fixture()
            : base(new EmptyScope())
        {
        }
    }
}