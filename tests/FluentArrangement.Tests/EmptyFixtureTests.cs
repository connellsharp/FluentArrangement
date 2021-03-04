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
        [MemberData(nameof(TestCases.Ints))]
        public void CreatesNumberUsingRegisteredValue(int number)
        {
            _fixture.RegisterType<int>(number);

            var result = _fixture.Create<int>();

            result.Should().Be(number);
        }

        [Theory]
        [MemberData(nameof(TestCases.Strings))]
        public void CreatesStringUsingRegisteredString(string text)
        {
            _fixture.RegisterType<string>(text);

            var result = _fixture.Create<string>();

            result.Should().Be(text);
        }
    }
}
