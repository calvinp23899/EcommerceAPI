using EcommerceAPI.Interface;
using EcommerceAPI.Repository;

namespace EcommerceAPI.Extensions
{
    public static class HealthcheckConnectionDatabaseExtension
    {
        public static void HealthCheckDBConnection(this WebApplication app, ILoggerManager logger)
        {
            using (var scope = app.Services.CreateScope())
            {
                try
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<RepositoryContext>();
                    // Try to open a connection to the database
                    dbContext.Database.CanConnect();
                    logger.LogInfo("Database connection successful.");
                }
                catch (Exception ex)
                {
                    logger.LogWarn($"Database connection failed: {ex.Message}");
                    throw new Exception("Database connection failed");
                }
            }
        }
    }
}
