using FluentAssertions;
using Xunit;

namespace FluentArrangement.Tests
{
    public class InterfaceFixtureTests
    {
        private readonly IFixture _fixture;

        public InterfaceFixtureTests()
        {
            _fixture = new Fixture().UseProxyObjects();
        }

        public interface INumberGenerator
        {
            int GetNumber();

            string GetText();

            string TextProperty { get; }

            int GetNumberWithArg(decimal arg);
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
            new object[] { "This is a longer string that contains more characters" }
        };

        [Theory]
        [MemberData(nameof(IntTestCases))]
        public void ProxiedMethodReturnsRegisteredNumber(int number)
        {
            _fixture.UseInstance<int>(number);

            var generator = _fixture.Create<INumberGenerator>();
            var result = generator.GetNumber();

            result.Should().Be(number);
        }

        [Theory]
        [MemberData(nameof(IntTestCases))]
        public void ProxiedMethodWithArgReturnsRegisteredNumber(int number)
        {
            _fixture.UseInstance<int>(number);

            var generator = _fixture.Create<INumberGenerator>();
            var result = generator.GetNumberWithArg(13.37m);

            result.Should().Be(number);
        }

        [Theory]
        [MemberData(nameof(StringTestCases))]
        public void ProxiedMethodReturnsRegisteredString(string text)
        {
            _fixture.UseInstance<string>(text);

            var generator = _fixture.Create<INumberGenerator>();
            var result = generator.GetText();

            result.Should().Be(text);
        }

        [Theory]
        [MemberData(nameof(StringTestCases))]
        public void ProxiedPropertyReturnsRegisteredString(string text)
        {
            _fixture.UseInstance<string>(text);

            var generator = _fixture.Create<INumberGenerator>();
            var result = generator.TextProperty;

            result.Should().Be(text);
        }
    }
}
