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

Factory priority is in reverse order, so if many factories can create the same type, the last one registered will be used.

```c#
[Fact]
public Task GetReturns200WhenFlagIsTrue()
{
    Fixture.ForProperty((MySettings s) => s.MyFlag,
                            f => f.UseValue(true));

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

## Wait... haven't I seen this before?

This is largely influenced by [AutoFixture](https://github.com/AutoFixture/AutoFixture) and [AutoFixture.AutoMoq](https://github.com/AutoFixture/AutoFixture/tree/master/Src/AutoMoq), which I've used daily for several years. I love it, but I've found some concepts difficult to learn and to teach, so I'd like to try simplify those ideas.

1. **Everything is a factory**. All creation algorithms are explicitly registered. Add some of the built-in randomisation, or use your own, or configure a combination.
2. **No need for Moq**. The type interception factory just returns from the fixture. It even works with generics.
3. **Monitoring is built-in**. No need to Freeze Mocks and Verify. Just act, then assert against the traced requests to the factories.
4. **Scopable**. Apply factories to types nested inside others. Useful when your dependency's dependencies depend on certain settings.
5. **Natural configuration**. The fluent interface means less documentation and more intellisense.