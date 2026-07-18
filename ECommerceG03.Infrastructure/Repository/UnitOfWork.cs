using ECommerceG03.Domain.Contracts;
using ECommerceG03.Domain.Entities;
using ECommerceG03.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Infrastructure.Repository
{
    public class UnitOfWork(StoreDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var typeName = typeof(TEntity).Name;
            if (!_repositories.TryGetValue(typeName, out var repository))
            {
                repository = new GenericRepository<TEntity, TKey>(_dbContext);
                _repositories[typeName] = repository;
            }
            return (IGenericRepository<TEntity, TKey>)repository;
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
            => await _dbContext.SaveChangesAsync(ct);
    }
}
