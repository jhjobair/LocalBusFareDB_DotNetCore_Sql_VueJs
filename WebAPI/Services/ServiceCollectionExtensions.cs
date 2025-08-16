namespace WebApi.Services
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Application services registration
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IStationsService, StationsService>();

            return services;
        }
    }
}
