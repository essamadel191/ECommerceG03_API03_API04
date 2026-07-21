using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Application.Common
{
    public class ProductQueryParams
    {
        public int? brandId { get; set; }
        public int? typeId { get; set; }
        public string? search { get; set; }

        public ProductSortOptions sort { get; set; } = ProductSortOptions.None;

        private const int maxPageSize = 20;
        private const int minPageSize = 5;

        public int pageIndex { get; set; } = 1;
        private int? pageSize;

        public int PageSize
        {
            get => pageSize ?? maxPageSize;
            set => pageSize = value > maxPageSize ? maxPageSize : (value < 1 ? minPageSize : value);
        }

    }
}
