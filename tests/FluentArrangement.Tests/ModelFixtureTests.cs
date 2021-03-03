using FluentAssertions;
using Xunit;

namespace FluentArrangement.Tests
{
    public class ModelFixtureTests
    {
        private readonly IFixture _fixture;

        public ModelFixtureTests()
        {
            _fixture = new Fixture().Register(new CtorAndPropsFactory());
        }

        private class NumberModel
        {
            public int Number { get; set; }
        }

        [Theory]
        [InlineData(42)]
        [InlineData(1337)]
        [InlineData(5318008)]
        public void SetsNumericProperty(int number)
        {
            _fixture.RegisterType<int>(number);

            var result = _fixture.Create<NumberModel>();

            result.Number.Should().Be(number);
        }
    }
}
