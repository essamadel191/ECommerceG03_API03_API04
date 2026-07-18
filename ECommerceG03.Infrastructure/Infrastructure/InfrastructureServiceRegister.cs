using ECommerceG03.Domain.Contracts;
using ECommerceG03.Infrastructure.Data;
using ECommerceG03.Infrastructure.DataSeeding;
using ECommerceG03.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Infrastructure.Infrastructure
{
    public static class InfrastructureServiceRegister
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register your infrastructure services here
            // For example, if you have a repository or a database context, you can register them like this:
            //services.AddScoped<IYourRepository, YourRepositoryImplementation>();
            services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddKeyedScoped<IDataSeeder, CatalogDataSeed>("Catalog");
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            //services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
