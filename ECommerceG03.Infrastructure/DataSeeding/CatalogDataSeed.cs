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
                var pendingMigratios = await _dbContext.Database.GetPendingMigrationsAsync();
                if (pendingMigratios.Any())
                {
                    await _dbContext.Database.MigrateAsync(ct);
                }

                var rootPath = Path.Combine(AppContext.BaseDirectory, "DataSeed");

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
                _logger.LogError(ex.Message);
            }
        }

        // Helper method to read data
        private async Task SeedDataIfEmptyAsync<T, Tkey>(string rootPath, string fileName, CancellationToken ct = default) where T : BaseEntity<Tkey>
        {
            if (await _dbContext.Set<T>().AnyAsync())
            {
                return;
            }

            var filePath = Path.Combine(rootPath, fileName);
            if (!File.Exists(filePath))
            {
                return;
            }

            var fileStream = File.OpenRead(filePath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var items = await JsonSerializer.DeserializeAsync<List<T>>(fileStream, options);

            if (items?.Any() ?? false)
            {
                await _dbContext.Set<T>().AddRangeAsync(items, ct);
                await _dbContext.SaveChangesAsync(ct);
            }
        }
    }
}
