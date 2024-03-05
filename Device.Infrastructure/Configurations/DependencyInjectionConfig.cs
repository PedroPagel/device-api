using Device.Domain.Interfaces;
using Device.Domain.Interfaces.Repositories;
using Device.Domain.Interfaces.Services;
using Device.Domain.Middlewares;
using Device.Domain.Services;
using Device.Infrastructure.DbContexts;
using Device.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Device.Infrastructure.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddDbContext<DeviceContext>(config =>
            {
                config.UseInMemoryDatabase("device-database");
            });

            services.AddSingleton<ExceptionHandlerMiddleware>();
            services.AddLogging();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IDeviceService, DeviceService>();
            services.AddScoped<INotificator, Notificator>();

            return services;
        }
    }
}
