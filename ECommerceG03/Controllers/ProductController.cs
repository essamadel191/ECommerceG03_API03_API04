using ECommerceG03.Application.Common;
using ECommerceG03.Application.Contracts;
using ECommerceG03.Application.DTOs.ProductDtos;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceG03.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ApiBaseController
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Get all products
        /// BaseURL/api/product
        /// </summary>
        /// <param name="ct" optional="true">Cancellation token</param>
        /// <returns>List of products</returns>
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAllProducts(CancellationToken ct = default)
        {
            var result = await _productService.GetAllProductsAsync(ct);
            return ToActionResult(result);
        }

        /// <summary>
        /// Get product by ID
        /// BaseURL/api/product/{id}
        /// </summary>
        /// <param name="ct" optional="true">Cancellation token</param>
        /// <param name="id">Product ID</param>
        /// <returns>Product Details</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id, CancellationToken ct = default)
        {
            var result = await _productService.GetProductByIdAsync(id, ct);
            return ToActionResult(result);
        }

        /// <summary>
        /// Get All Brands
        /// BaseURL/api/products/brands
        /// </summary>
        /// <param name="ct" optional="true">Cancellation token</param>
        /// <returns>List of brands</returns>
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<BrandDto>>> GetAllBrands(CancellationToken ct = default)
        {
            var result = await _productService.GetAllBrandAsync(ct);
            return ToActionResult(result);
        }

        /// <summary>
        /// Get All Types
        /// BaseURL/api/products/types
        /// </summary>
        /// <param name="ct" optional="true">Cancellation token</param>
        /// <returns>List of types</returns>
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<TypeDto>>> GetAllTypes(CancellationToken ct = default)
        {
            var result = await _productService.GetAllTypeAsync(ct);
            return ToActionResult(result);
        }

    }
}
