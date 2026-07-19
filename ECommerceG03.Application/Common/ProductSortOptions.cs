using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace ECommerceG03.Application.Common
{
    [JsonConverter(typeof(JsonStringEnumConverter<ProductSortOptions>))]
    public enum ProductSortOptions
    {
        None = 0,
        NameAsc = 1,
        NameDesc = 2,
        PriceAsc = 3,
        PriceDesc = 4
    }
}
