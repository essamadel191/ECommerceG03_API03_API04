using ECommerceG03.Domain.Contracts;
using ECommerceG03.Domain.Entities;
using ECommerceG03.Domain.Entities.Products;
using ECommerceG03.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace ECommerceG03.Infrastructure.DataSeeding
{
    public class CatalogDataSeed(StoreDbContext _dbContext, ILogger<CatalogDataSeed> _logger) : IDataSeeder
    {
        public async Task SeedDataAsync(CancellationToken ct = default)
        {
            try
            {
                _logger.LogInformation("Starting data seeding process...");

                var pendingMigratios = await _dbContext.Database.GetPendingMigrationsAsync();
                if (pendingMigratios.Any())
                {
                    _logger.LogInformation("Applying pending migrations...");
                    await _dbContext.Database.MigrateAsync(ct);
                }

                var rootPath = Path.Combine(AppContext.BaseDirectory, "DataSeed");
                _logger.LogInformation($"DataSeed root path: {rootPath}");
                _logger.LogInformation($"Directory exists: {Directory.Exists(rootPath)}");

                await SeedDataIfEmptyAsync<ProductBrand, int>(rootPath, "brands.json", ct);
                await SeedDataIfEmptyAsync<ProductType, int>(rootPath, "types.json", ct);
                await SeedDataIfEmptyAsync<Product, int>(rootPath, "products.json", ct);

                var result = await _dbContext.SaveChangesAsync(ct);
                if (result > 0)
                {
                    _logger.LogInformation($"Data Seeded Successfully, {result} records affected.");
                }
                else
                {
                    _logger.LogInformation("No data was seeded.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during data seeding");
                throw;
            }
        }

        // Helper method to read data
        private async Task SeedDataIfEmptyAsync<T, Tkey>(string rootPath, string fileName, CancellationToken ct = default) where T : BaseEntity<Tkey>
        {
            var entityName = typeof(T).Name;

            if (await _dbContext.Set<T>().AnyAsync())
            {
                _logger.LogInformation($"Skipping {entityName} - table already has data");
                return;
            }

            var filePath = Path.Combine(rootPath, fileName);
            if (!File.Exists(filePath))
            {
                _logger.LogWarning($"Seed file not found: {filePath}");
                return;
            }

            _logger.LogInformation($"Reading seed data from: {filePath}");
            var fileStream = File.OpenRead(filePath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var items = await JsonSerializer.DeserializeAsync<List<T>>(fileStream, options);

            if (items?.Any() ?? false)
            {
                _logger.LogInformation($"Adding {items.Count} {entityName} records");
                await _dbContext.Set<T>().AddRangeAsync(items, ct);
            }
            else
            {
                _logger.LogWarning($"No items found in {filePath}");
            }
        }
    }
}
