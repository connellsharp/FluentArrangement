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

        [Theory]
        [MemberData(nameof(TestCases.Ints))]
        public void SetsNumericProperty(int number)
        {
            _fixture.RegisterType<int>(number);

            var generator = _fixture.Create<INumberGenerator>();
            var result = generator.GetNumber();

            result.Should().Be(number);
        }
    }
}
