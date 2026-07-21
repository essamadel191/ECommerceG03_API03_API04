using ECommerceG03.Application.Common;
using ECommerceG03.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ECommerceG03.Application.Specification
{
    public class ProductCountSpecification : BaseSpecification<Product, int>
    {
        public ProductCountSpecification(ProductQueryParams queryParams) : base
            (p => (!queryParams.brandId.HasValue || p.BrandId == queryParams.brandId) 
            && (!queryParams.typeId.HasValue || p.TypeId == queryParams.typeId) 
            && (string.IsNullOrWhiteSpace(queryParams.search) || p.Name.ToLower().Contains(queryParams.search.ToLower())))
        {
            // Nothing for the count for now.
        }
    }
}
