using System.Collections.Generic;

namespace JQ.Common.Model
{
    public class PaginationBase
    {
       // public bool IsPagination { get; set; }
       
        public int PageIndex { get; set; } = 1;

        private int _pageSize = 10;
        protected int MaxPageSize { get; set; } = 100;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }

        public string OrderBy { get; set; }

    }
}
