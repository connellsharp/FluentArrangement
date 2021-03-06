using FluentAssertions;
using Xunit;

namespace FluentArrangement.Tests
{
    public class ConstructorFixtureTests
    {
        private readonly IFixture _fixture;

        public ConstructorFixtureTests()
        {
            _fixture = new Fixture().UseModels();
        }

        private class TestModel
        {
            public TestModel(int number, string text)
            {
                Number = number;
                Text = text;
            }

            public int Number { get; }

            public string Text { get; }
        }

        private class TestParentModel
        {
            public TestParentModel(TestModel childModel, string text)
            {
                ChildModel = childModel;
                ParentText = text;
            }

            public TestModel ChildModel { get; }
            
            public string ParentText { get; }
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
        public void SetsNumericProperty(int number)
        {
            _fixture.UseInstance<int>(number);

            var result = _fixture.Create<TestModel>();

            result.Number.Should().Be(number);
        }

        [Theory]
        [MemberData(nameof(IntTestCases))]
        public void SetsNumericPropertiesOnNestedModels(int number)
        {
            _fixture.UseInstance<int>(number);

            var result = _fixture.Create<TestParentModel>();

            result.ChildModel.Number.Should().Be(number);
        }

        [Theory]
        [MemberData(nameof(StringTestCases))]
        public void SetsStringProperty(string text)
        {
            _fixture.UseInstance<string>(text);

            var result = _fixture.Create<TestModel>();

            result.Text.Should().Be(text);
        }

        [Theory]
        [MemberData(nameof(StringTestCases))]
        public void SetsStringPropertiesOnNestedModels(string text)
        {
            _fixture.UseInstance<string>(text);

            var result = _fixture.Create<TestParentModel>();

            result.ChildModel.Text.Should().Be(text);
        }
    }
}
