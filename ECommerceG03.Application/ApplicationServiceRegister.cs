using ECommerceG03.Application.Contracts;
using ECommerceG03.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceG03.Application
{
    public static class ApplicationServiceRegister
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(c => { }, typeof(ApplicationServiceRegister).Assembly);
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
