# FluentArrangement

FluentArrangement is a DI container designed for use in the 'arrange' phase of a unit test.

It provides a fluent interface to configure auto mocking and random test data generation.


```c#
private IFixture Fixture = new Fixture()
    .UseDefaults()
    .UseConstructorsAndProperties()
    .UseProxyObjects()
    .ForType<MyOtherType>(f => f
        .UseRandomValues())
    .Use(new MyCustomFactory());
```

Use it to generate your SUT (System Under Test).

```c#
[Fact]
public Task GetReturns404()
{
    var controller = Fixture.Create<MyController>();

    var result = await controller.GetAsync("id");

    Assert.Equals(404, result.StatusCode);
}
```

Further customize the fixture in a test method.

```c#
[Fact]
public Task GetReturns200WhenFlagIsTrue()
{
    Fixture.ForProperty((MySettings s) => s.MyFlag, f => f
        .UseValue(true));

    var controller = Fixture.Create<MyController>();

    var result = await controller.GetAsync("id");

    Assert.Equals(200, result.StatusCode);
}
```

All requests are monitored, allowing you to verify calls to dependencies.

Combine with [FluentAssertions](https://fluentassertions.com/) for maximum fluentness.

```c#
[Fact]
public Task GetCallsRepositoryOnce()
{
    var controller = Fixture.Create<MyController>();

    var result = await controller.GetAsync("id");

    Fixture.Requests.GetMethodCalls()
        .ToType<IMyRepository>()
        .Should().ContainSingle()
        .Which.Method.Name.Should().Be("Add");
}
```