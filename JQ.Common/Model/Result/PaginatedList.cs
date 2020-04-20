using System;
using System.Collections.Generic;
using System.Text;

namespace JQ.Common.Model
{
    public class PaginatedList<TEntity>
    {

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public List<TEntity> PageList { get; set; }

        public int Total { get; set; }

        public PaginatedList()
        {
           
        }
        public PaginatedList(int pageIndex, int pageSize, int total, List<TEntity> pageList)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Total = total;
            PageList = pageList;
        }

    }
}
