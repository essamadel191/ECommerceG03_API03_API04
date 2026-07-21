using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceG03.Application.Common
{
    public class PaginatedResult<TEntity>
    {
        public IReadOnlyList<TEntity> Data { get; }
        public int PageIndex { get; }
        public int PageSize { get; }
        public int Count { get; }
        public PaginatedResult(IReadOnlyList<TEntity> _Data, int _PageIndex, int _PageSize, int _Count)
        {
            Data = _Data;
            PageIndex = _PageIndex;
            PageSize = _PageSize;
            Count = _Count;
        }

    }
}
