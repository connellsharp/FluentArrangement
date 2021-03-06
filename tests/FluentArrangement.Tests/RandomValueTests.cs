using System;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace FluentArrangement.Tests
{
    public class RandomValueTests
    {
        private readonly IFixture _fixture;

        public RandomValueTests()
        {
            _fixture = new Fixture().UseRandomValues();
        }

        [Theory]
        [InlineData(typeof(string))]
        // [InlineData(typeof(char))]
        [InlineData(typeof(byte))]
        [InlineData(typeof(sbyte))]
        [InlineData(typeof(int))]
        [InlineData(typeof(uint))]
        [InlineData(typeof(long))]
        [InlineData(typeof(ulong))]
        [InlineData(typeof(float))]
        [InlineData(typeof(double))]
        [InlineData(typeof(decimal))]
        // [InlineData(typeof(Guid))]
        // [InlineData(typeof(DateTime))]
        // [InlineData(typeof(DateTimeOffset))]
        // [InlineData(typeof(TimeSpan))]
        // [InlineData(typeof(Uri))]
        public void CreatingValueTwiceReturnsDifferentResults(Type type)
        {
            var result1 = _fixture.Create(type);
            var result2 = _fixture.Create(type);
            
            result1.Should().NotBeNull()
                .And.NotBe(result2);
        }

        [Fact]
        public void BooleansAreDistributedEvenly()
        {
            var results = Enumerable.Repeat(false, 1000)
                        .Select(_ => _fixture.Create<bool>());

            results.Count(b => b == true).Should().BeCloseTo(500, 50);
        }
    }
}
