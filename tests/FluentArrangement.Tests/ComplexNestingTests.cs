using FluentAssertions;
using Xunit;

namespace FluentArrangement.Tests
{
    public class ComplexNestingTests
    {
        private readonly IFixture _fixture;

        public ComplexNestingTests()
        {
            _fixture = new Fixture().UseDefaults()
                                    .UseConstructorAndSetProperties()
                                    .UseProxyObjects();
        }

        public class TestModel
        {
            public int Number { get; set; }

            public string Text { get; set; }
        }

        public class TestParentModel
        {
            public TestParentModel(TestModel childModel)
                => ChildModel = childModel;

            public TestModel ChildModel { get; }
            
            public string ParentText { get; set; }
        }

        public class TestGrandParentModel
        {
            public TestParentModel ChildModel { get; set; }
            
            public string GrandParentText { get; set; }
        }

        public interface ITestInterface
        {
            TestGrandParentModel GetModel(string arg);
        }

        [Fact]
        public void GetsNumberThroughSeveralLayersOfNesting()
        {
            _fixture.UseInstance<int>(9876);

            var sut = _fixture.Create<ITestInterface>();

            var result = sut.GetModel("test").ChildModel.ChildModel.Number;

            result.Should().Be(9876);
        }
    }
}
