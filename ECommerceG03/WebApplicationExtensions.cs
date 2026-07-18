using ECommerceG03.Domain.Contracts;

namespace ECommerceG03
{
    public static class WebApplicationExtensions
    {
        public static async Task<WebApplication> SeedAndMigrateDataAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dataSeeder = scope.ServiceProvider.GetRequiredKeyedService<IDataSeeder>("Catalog");

            await dataSeeder.SeedDataAsync();
            return app;
        }
    }
}
