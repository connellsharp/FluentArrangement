using System;

namespace FluentArrangement
{
    public interface IFixture
    {
        void Register(IFactory factory);

        IFixture NewScope();

        object? Create(Type type);

        RequestCollection Requests { get; }
    }
}