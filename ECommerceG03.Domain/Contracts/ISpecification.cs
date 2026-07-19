using ECommerceG03.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ECommerceG03.Domain.Contracts
{
    public interface ISpecification<TEntity,TKey> where TEntity : BaseEntity<TKey>
    {
        // Include
        ICollection<Expression<Func<TEntity, object>>> IncludeExpressions { get; }

        // Where
        Expression<Func<TEntity, bool>>? Criteria { get; }

        // OrderBy
        Expression<Func<TEntity, object>> OrderBy { get; }
        Expression<Func<TEntity, object>> OrderByDescending { get; }

        //Pagination
        int Skip { get; }
        int Take { get; }
        bool IsPagingEnabled { get; }
    }
}
