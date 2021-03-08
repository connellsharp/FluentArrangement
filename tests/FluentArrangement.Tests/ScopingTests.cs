using FluentAssertions;
using Xunit;

namespace FluentArrangement.Tests
{
    public class ScopingTests
    {
        private readonly IFixture _fixture = new Fixture().UseInstance<decimal>(4.2m);

        public static object[][] DecimalTestCases = new[]
        {
            new object[] { 42m },
            new object[] { 13.37m },
            new object[] { 123456.654321 }
        };

        [Fact]
        public void NoScopeReturnsParent()
        {
            var result = _fixture.Create<decimal>();

            result.Should().Be(4.2m);
        }

        [Fact]
        public void InnerScopeReturnsFromParentIfNothingElseIsRegistered()
        {
            var scopedFixture = _fixture.NewScope();

            var result = scopedFixture.Create<decimal>();

            result.Should().Be(4.2m);
        }

        [Theory]
        [MemberData(nameof(DecimalTestCases))]
        public void InnerScopeReturnsValueRegisteredInsideScope(decimal number)
        {
            var scopedFixture = _fixture.NewScope();

            scopedFixture.UseInstance<decimal>(number);

            var result = scopedFixture.Create<decimal>();

            result.Should().Be(number);
        }

        [Fact]
        public void ParentScopeIgnoresRegistrationFromInnerScope()
        {
            var scopedFixture = _fixture.NewScope();

            scopedFixture.UseInstance<decimal>(13.37m);

            var result = _fixture.Create<decimal>();

            result.Should().Be(4.2m);
        }
    }
}
