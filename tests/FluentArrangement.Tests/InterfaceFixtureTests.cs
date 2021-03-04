using FluentAssertions;
using Xunit;

namespace FluentArrangement.Tests
{
    public class InterfaceFixtureTests
    {
        private readonly IFixture _fixture;

        public InterfaceFixtureTests()
        {
            _fixture = new Fixture().Register(new MockEverythingFactory());
        }

        public interface INumberGenerator
        {
            int GetNumber();
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
        public void SetsNumericProperty(int number)
        {
            _fixture.RegisterType<int>(number);

            var generator = _fixture.Create<INumberGenerator>();
            var result = generator.GetNumber();

            result.Should().Be(number);
        }
    }
}
