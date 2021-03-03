using System;
using Xunit;

namespace FluentArrangement.Tests
{
    public class BasicTests
    {
        private readonly IFixture _fixture;

        public BasicTests()
        {
            _fixture = new Fixture()
                .Register(new CtorAndPropsFactory())
                .Register(new MockEverythingFactory())
                .RegisterType<MyType>(c => new MyType(c.Resolve<string>()))
                .RegisterType<IMyType>(c => c.Resolve<MyType>())
                .Register(new ParameterFactory<string>("userId", "123456"))
                .RegisterParameter<string>("userId", "123456");
            
        }

        [Fact]
        public void Demo()
        {
            Given.The<ITestRepository>.Returns(new TestObject());
            Given.The<TestObject>.HasProperty(t => t.MyFlag).SetTo(true);

            var request = _fixture.Create<MyWhateverRequest>();
            var sut = _fixture.Create<MyWhateverController>();
        }
    }
}
