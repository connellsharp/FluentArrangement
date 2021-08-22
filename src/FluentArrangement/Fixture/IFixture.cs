using System;

namespace FluentArrangement
{
    public interface IFixture
    {
        void Register(IFactory factory);

        object? Create(Type type);

        IFixture NewScope();
    }

    public class Fixture : ScopeFixture
    {
        public Fixture()
            : base(new FactoryScope(new VoidFactory()))
        {
        }
    }
}