using ECommerceG03.Domain.Contracts;
using ECommerceG03.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Infrastructure.Specification
{
    public static class SpecificationEvaluator
    {
        public static IQueryable<TEntity> CreateQuery<TEntity, Tkey>(IQueryable<TEntity> endPoint,
            ISpecification<TEntity,Tkey> specification) where TEntity : BaseEntity<Tkey>
        {
            //1. Entry Point
            var query = endPoint;

            //2. Where clause and Include
            if(specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            #region Old Loop
            //3. Include clause
            //if(specification.IncludeExpressions.Any())
            //{
            //    foreach (var includeExpression in specification.IncludeExpressions)
            //    {
            //        query = query.Include(includeExpression);
            //    }
            //} 
            #endregion

            query = specification.IncludeExpressions.Aggregate(query, (current, includeExpression) => current.Include(includeExpression));

            //3. OrderBy clause
            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            //4. Pagination
            if(specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Skip).Take(specification.Take);
            }

            return query;
        }
    }
}
