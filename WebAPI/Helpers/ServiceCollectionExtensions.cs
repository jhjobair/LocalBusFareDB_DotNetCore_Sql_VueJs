using WebApi.Interface;
using WebApi.Services;

namespace WebApi.Helpers
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Application services registration
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IStationsService, StationsService>();
            services.AddScoped<IChartInfoService, ChartInfoService>();
            services.AddScoped<IFareChartService, FareChartService>();

            return services;
        }
    }
}
