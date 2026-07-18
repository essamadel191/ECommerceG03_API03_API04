using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Domain.Contracts
{
    public interface IDataSeeder
    {
        Task SeedDataAsync(CancellationToken ct = default);
    }
}
