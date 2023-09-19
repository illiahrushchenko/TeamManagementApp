using Application.Common.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Testcontainers.PostgreSql;

namespace Application.IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("team_management_test")
        .WithUsername("postgres")
        .WithPassword("1234")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        base.ConfigureWebHost(builder);

        _dbContainer.StartAsync().Wait();

        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContextOptions<ApplicationDbContext>>()
                .AddDbContext<ApplicationDbContext>(options =>
                    options.UseNpgsql(_dbContainer.GetConnectionString()));

            services.RemoveAll<ICurrentUserService>()
                .AddTransient(provider => Mock.Of<ICurrentUserService>(s =>
                    s.UserId == Testing.GetUserId()));
        });
    }

    public async Task StartDbContainerAsync()
    {
        await _dbContainer.StartAsync();
    }

    public async Task DisposeDbContainerAsync()
    {
        await _dbContainer.DisposeAsync();
    }
}