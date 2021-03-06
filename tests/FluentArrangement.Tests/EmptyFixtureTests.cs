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

        public static object[][] IntTestCases = new[]
        {
            new object[] { 42 },
            new object[] { 1337 },
            new object[] { 5318008 }
        };
        
        public static object[][] StringTestCases = new[]
        {
            new object[] { "" },
            new object[] { "Test" },
            new object[] { "" }
        };

        [Theory]
        [MemberData(nameof(IntTestCases))]
        public void CreatesNumberUsingRegisteredValue(int number)
        {
            _fixture.UseInstance<int>(number);

            var result = _fixture.Create<int>();

            result.Should().Be(number);
        }

        [Theory]
        [MemberData(nameof(StringTestCases))]
        public void CreatesStringUsingRegisteredString(string text)
        {
            _fixture.UseInstance<string>(text);

            var result = _fixture.Create<string>();

            result.Should().Be(text);
        }
    }
}
