using ECommerceG03.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Domain.Contracts
{
    public interface IGenericRepository<TEntity , Tkey> where TEntity : BaseEntity<Tkey>
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);

        Task<TEntity?> GetByIdAsync(Tkey id,CancellationToken ct = default);
        Task<TEntity?> GetByIdAsync(Tkey id, ISpecification<TEntity, Tkey> specification, CancellationToken ct = default);
        Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default);

        // Get all entities based on a specification
        Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecification<TEntity, Tkey> specification, CancellationToken ct = default);

        // Count
        Task<int> CountAsync(ISpecification<TEntity, Tkey> specification, CancellationToken ct = default);
    }
}
