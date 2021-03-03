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

        [Fact]
        public void CreatesNumberUsingRegisteredValue()
        {
            _fixture.RegisterType<int>(42);

            var result = _fixture.Create<int>();

            result.Should().Be(42);
        }

        [Fact]
        public void CreatesObjectWithMethodThatReturnsRegisteredValue()
        {
            _fixture.RegisterType<int>(42);

            var sut = _fixture.Create<INumberGenerator>();

            var result = sut.GetNumber();

            result.Should().Be(42);
        }

        private interface INumberGenerator
        {
            int GetNumber();
        }
    }
}
