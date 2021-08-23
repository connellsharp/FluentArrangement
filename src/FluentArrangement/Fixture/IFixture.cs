using System;
using System.Collections.Generic;

namespace FluentArrangement
{
    public interface IFixture
    {
        void Register(IFactory factory);

        IFixture NewScope();

        object? Create(Type type);

        IEnumerable<MonitoredRequest> Requests { get; }
    }
}