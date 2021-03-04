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

        private class TestModel
        {
            public int Number { get; set; }
            public string Text { get; set; }
        }

        [Theory]
        [InlineData(42)]
        [InlineData(1337)]
        [InlineData(5318008)]
        public void SetsNumericProperty(int number)
        {
            _fixture.RegisterType<int>(number);

            var result = _fixture.Create<TestModel>();

            result.Number.Should().Be(number);
        }

        [Theory]
        [InlineData("")]
        [InlineData("Test")]
        [InlineData("Longer string with some words")]
        public void SetsStringProperty(string text)
        {
            _fixture.RegisterType<string>(text);

            var result = _fixture.Create<TestModel>();

            result.Text.Should().Be(text);
        }
    }
}
