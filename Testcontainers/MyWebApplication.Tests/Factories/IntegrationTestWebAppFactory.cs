using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyWebApplication.Api.Infrastructure;
using MyWebApplication.Tests.Extensions;
using Testcontainers.PostgreSql;
using Xunit;

namespace MyWebApplication.Tests.Factories;

public class IntegrationTestWebAppFactory
    : WebApplicationFactory<Program>,
        IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithDatabase("TestDatabase")
        .WithUsername("TestUser")
        .WithPassword("TestPassword@1234!")
        .WithImage("postgres:11")
        .WithCleanUp(true)
        .Build();

    protected override void ConfigureWebHost(
        IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptorType =
                typeof(DbContextOptions<MyDbContext>);

            var descriptor = Enumerable
                .SingleOrDefault(services, s => s.ServiceType == descriptorType);

            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }


            services.AddDbContext<MyDbContext>(options =>
                options.UseNpgsql(_dbContainer.GetConnectionString()));

            // Ensure schema gets created
            services.EnsureDbCreated<MyDbContext>();
        });

    }

    public Task InitializeAsync()
    {
        return _dbContainer.StartAsync();
    }

    public new Task DisposeAsync()
    {
        return _dbContainer.StopAsync();
    }
}
