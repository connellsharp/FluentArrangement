using FluentAssertions;
using Xunit;

namespace FluentArrangement.Tests
{
    public class MonitoringTests
    {
        private readonly IFixture _fixture = new Fixture().UseConstructorAndSetProperties()
                                                          .UseProxyObjects();

        public class TestEntity
        {
            public int TestNumber { get; set; }
        }

        public interface ITestRepository
        {
            public void Add(TestEntity entity);
        }

        public class TestController
        {
            private ITestRepository _repository;

            public TestController(ITestRepository repository)
                => _repository = repository;

            public void Post(int number)
                => _repository.Add(new TestEntity { TestNumber = number });
        }

        [Theory]
        [InlineData(42)]
        [InlineData(1337)]
        public void MonitorsMethodCall(int number)
        {
            var controller = _fixture.Create<TestController>();
            
            controller.Post(number);

            _fixture.Requests.GetMethodCalls()
                .ForType<ITestRepository>()
                .ToMethod(nameof(ITestRepository.Add))
                .Should().ContainSingle()
                .Which.Arguments.Should().ContainSingle()
                .Which.Should().BeOfType<TestEntity>()
                .Which.TestNumber.Should().Be(number);
        }

        [Theory]
        [InlineData(6)]
        [InlineData(17)]
        public void MonitorsMultipleIdenticalMethodCalls(int count)
        {
            var controller = _fixture.Create<TestController>();
            
            for(int i = 0; i < count; i++)
            {
                controller.Post(7654);
            }

            _fixture.Requests.GetMethodCalls()
                .ForType<ITestRepository>()
                .ToMethod(nameof(ITestRepository.Add))
                .Should().HaveCount(count);
        }
    }
}
