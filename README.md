# FluentArrangement

*This project is still just an idea at the moment.*

FluentArrangement is a DI container designed for use in the 'arrange' phase of a unit test.

```c#
public class MyWhateverControllerTests
{
    private readonly IFixture _fixture;

    public MyWhateverControllerTests()
    {
        _fixture = new Fixture()
            .Register(new CtorAndPropsFactory())
            .Register(new MockEverythingFactory())
            .RegisterType<MyType>(c => new MyType(c.Resolve<string>()))
            .RegisterType<IMyType>(c => c.Resolve<MyType>())
            .Register(new ParameterFactory<string>("userId", "123456"))
            .RegisterParameter<string>("userId", "123456");
    }

    [Fact]
    public Task GetReturns200WhenFlagIsTrue()
    {
        Given.The<ITestRepository>.Returns(new TestObject());
        Given.The<MySettings>.HasProperty(s => s.MyFlag).SetTo(true);

        var request = _fixture.Create<MyWhateverRequest>();
        var controller = _fixture.Create<MyWhateverController>();

        var result = await controller.GetAsync("id");

        Assert.Equals(200, result.StatusCode);
    }
}
```