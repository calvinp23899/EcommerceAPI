using EcommerceAPI.Interface;
using EcommerceAPI.Service.LoggerService;

namespace EcommerceAPI.ServicesExtension
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });
        }
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>{});
        }
        public static void ConfigureLoggerService(this IServiceCollection services) 
        {
            services.AddTransient<ILoggerManager, LoggerManager>();

        }
    }
}
