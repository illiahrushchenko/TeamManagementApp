using System.Reflection;
using Application.Common.Interfaces;
using Application.Common.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            // Add behaviors
        });

        services.AddScoped<IBoardMembersService, BoardMembersService>();
        
        return services;
    }
}