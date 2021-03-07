using FluentAssertions;
using Xunit;

namespace FluentArrangement.Tests
{
    public class MonitoringTests
    {
        private readonly IFixture _fixture;

        public MonitoringTests()
        {
            _fixture = new Fixture().UseConstructorAndSetProperties()
                                    .UseProxyObjects();
        }

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
            var repoMonitor = _fixture.Monitor<ITestRepository>();

            var controller = _fixture.Create<TestController>();
            
            controller.Post(number);

            repoMonitor.CallsTo(nameof(ITestRepository.Add)).Should().ContainSingle()
                .Which.Arguments.Should().ContainSingle()
                .Which.Value.Should().BeOfType<TestEntity>()
                .Which.TestNumber.Should().Be(number);
        }
    }
}
