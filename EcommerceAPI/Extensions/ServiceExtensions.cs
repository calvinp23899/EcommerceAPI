using EcommerceAPI.Interface;
using EcommerceAPI.Interface.IRepository;
using EcommerceAPI.Interface.IService;
using EcommerceAPI.Repository;
using EcommerceAPI.Repository.ManagerRepository;
using EcommerceAPI.Service.LoggerService;
using EcommerceAPI.Service.ManagementService;
using Microsoft.EntityFrameworkCore;

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
        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }
        public static void ConfigureServiceManager(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
        }
        public static void ConfigureSqlContext(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(opts =>opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
        }
    }
}
