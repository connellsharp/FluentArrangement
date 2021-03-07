# FluentArrangement

FluentArrangement is a DI container designed for use in the 'arrange' phase of a unit test.

It can be used to generate random tests models and auto mocking.


```c#
public class MyControllerTests
{
    private readonly IFixture _fixture;

    public MyControllerTests()
    {
        _fixture = new Fixture()
            .UseDefaults()
            .UseConstructorsAndProperties()
            .UseProxyObjects()
            .For<MyOtherType>(f => f
                .UseRandomValues())
            .UseParameter<string>("userId", "123456")
            .Use(new MyCustomFactory());
    }

    [Fact]
    public Task GetReturns200WhenFlagIsTrue()
    {
        _fixture.UseProperty<MySettings>(s => s.MyFlag, true);

        var request = _fixture.Create<MyRequest>();
        var controller = _fixture.Create<MyController>();

        var result = await controller.GetAsync("id");

        Assert.Equals(200, result.StatusCode);
    }

    [Fact]
    public Task GetCallsRepositoryOnce()
    {
        var monitor = _fixture.Monitor<IMyRepository>();

        var request = _fixture.Create<MyRequest>();
        var controller = _fixture.Create<MyController>();

        var result = await controller.GetAsync("id");

        Assert.Single(monitor.CallsTo("Get"));
    }
}
```