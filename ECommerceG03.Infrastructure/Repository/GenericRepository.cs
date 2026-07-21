using ECommerceG03.Domain.Contracts;
using ECommerceG03.Domain.Entities;
using ECommerceG03.Infrastructure.Data;
using ECommerceG03.Infrastructure.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Infrastructure.Repository
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _dbContext;

        public GenericRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default)
            => await _dbContext.Set<TEntity>().ToListAsync(ct);
        public async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct = default)
            => await _dbContext.Set<TEntity>().FindAsync( id , ct);
          
        public void Add(TEntity entity)=> _dbContext.Set<TEntity>().Add(entity);
        public void Update(TEntity entity)=> _dbContext.Set<TEntity>().Update(entity);
        public void Remove(TEntity entity)=> _dbContext.Set<TEntity>().Remove(entity);

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecification<TEntity, TKey> specification, CancellationToken ct = default)
        {
            var query = SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specification);

            // 4. Execute query
            return await query.ToListAsync(ct);
        }

        public async Task<TEntity?> GetByIdAsync(TKey id, ISpecification<TEntity, TKey> specification, CancellationToken ct = default)
        {
            var query = SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specification);

            // 4. Execute query
            return await query.FirstOrDefaultAsync(ct);
        }

        public async Task<int> CountAsync(ISpecification<TEntity, TKey> specification, CancellationToken ct = default)
        {
            return await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specification).CountAsync(ct);
        }
    }
}