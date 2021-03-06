namespace FluentArrangement
{
    public interface IFixture
    {
        void Register(IFactory factory);

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