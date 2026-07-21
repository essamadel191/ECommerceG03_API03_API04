using ECommerceG03.Application.Common;
using ECommerceG03.Application.DTOs.ProductDtos;

namespace ECommerceG03.Application.Contracts
{
    public interface IProductService
    {
        //Task<Result<IReadOnlyList<ProductDto>>> GetAllProductsAsync(CancellationToken ct = default);
        Task<Result<PaginatedResult<ProductDto>>> GetAllProductsAsync(ProductQueryParams queryParams, CancellationToken ct = default);

        Task<Result<IReadOnlyList<BrandDto>>> GetAllBrandAsync(CancellationToken ct = default);
        Task<Result<IReadOnlyList<TypeDto>>> GetAllTypeAsync(CancellationToken ct = default);

        Task<Result<ProductDto>> GetProductByIdAsync(int productId, CancellationToken ct = default);


    }
}
