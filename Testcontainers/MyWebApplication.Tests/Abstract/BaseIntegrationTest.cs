using Microsoft.Extensions.DependencyInjection;
using MyWebApplication.Api.Infrastructure;
using MyWebApplication.Tests.Factories;
using Xunit;

namespace MyWebApplication.Tests.Abstract;

public abstract class BaseIntegrationTest
    : IClassFixture<IntegrationTestWebAppFactory>,
      IDisposable
{
    private readonly IServiceScope _scope;
    protected readonly MyDbContext DbContext;

    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();

        DbContext = _scope.ServiceProvider
            .GetRequiredService<MyDbContext>();
    }

    public void Dispose()
    {
        _scope?.Dispose();
        DbContext?.Dispose();
    }
}
