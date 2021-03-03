using FluentAssertions;
using Xunit;

namespace FluentArrangement.Tests
{
    public class EmptyFixtureTests
    {
        private readonly IFixture _fixture;

        public EmptyFixtureTests()
        {
            _fixture = new Fixture();
        }

        [Theory]
        [InlineData(42)]
        [InlineData(1337)]
        [InlineData(5318008)]
        public void CreatesNumberUsingRegisteredValue(int number)
        {
            _fixture.RegisterType<int>(number);

            var result = _fixture.Create<int>();

            result.Should().Be(number);
        }

        [Theory]
        [InlineData("")]
        [InlineData("Test")]
        [InlineData("Longer string with some words")]
        public void CreatesStringUsingRegisteredString(string text)
        {
            _fixture.RegisterType<string>(text);

            var result = _fixture.Create<string>();

            result.Should().Be(text);
        }
    }
}
