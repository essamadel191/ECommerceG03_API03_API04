using ECommerceG03.Application.Common;
using ECommerceG03.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Application.Specification
{
    public class ProductWihtBrandAndTypeSpec : BaseSpecification<Product, int>
    {
        public ProductWihtBrandAndTypeSpec(ProductQueryParams queryParams) : base
            (p => (!queryParams.brandId.HasValue || p.BrandId == queryParams.brandId) 
            && (!queryParams.typeId.HasValue || p.TypeId == queryParams.typeId) 
            && (string.IsNullOrWhiteSpace(queryParams.search) || p.Name.ToLower().Contains(queryParams.search.ToLower())))
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);

            switch (queryParams.sort)
            {
                case ProductSortOptions.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                case ProductSortOptions.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortOptions.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortOptions.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                default:
                    AddOrderBy(p => p.Id); 
                    break;
            }

            // Pagination
            applyPaging(queryParams.PageSize, queryParams.pageIndex);
        }
        public ProductWihtBrandAndTypeSpec(int productId) : base(p => p.Id == productId)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
