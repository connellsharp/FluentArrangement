using FluentAssertions;
using Xunit;

namespace FluentArrangement.Tests
{
    public class ForTypeTests
    {
        private readonly IFixture _fixture;

        public ForTypeTests()
        {
            _fixture = new Fixture()
                .UseConstructorAndSetProperties()
                .UseInstance<string>("OuterScopedString")
                .ForType<TestModel>(f => f
                    .UseConstructorAndSetProperties()
                    .UseInstance<string>("InnerScopedString"));
        }

        private class TestModel
        {
            public string Text { get; set; }
        }

        private class TestParentModel
        {
            public TestModel ChildModel { get; set; }
            
            public string ParentText { get; set; }
        }

        [Fact]
        public void ParentModelHasOuterScopedString()
        {
            var result = _fixture.Create<TestParentModel>();

            result.ParentText.Should().Be("OuterScopedString");
        }

        [Fact]
        public void ChildModelHasInnerScopedString()
        {
            var result = _fixture.Create<TestParentModel>();

            result.ChildModel.Text.Should().Be("InnerScopedString");
        }
    }
}
