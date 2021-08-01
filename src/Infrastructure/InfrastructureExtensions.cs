using Application;
using Application.Persistence;
using Application.Services;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastucture(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddApplication(configuration)
                //
                .AddDbContext<DataContext>(options => options.UseInMemoryDatabase(databaseName: "AlertTheLightning"))
                .AddScoped<IDataContext>(provider => provider.GetService<DataContext>())
                //
                .AddScoped<IEventDispatcherService, EventDispatcherService>()
                .AddScoped<IAlertNotificationService, AlertNotificationService>()
                ;

            return services;
        }
    }
}