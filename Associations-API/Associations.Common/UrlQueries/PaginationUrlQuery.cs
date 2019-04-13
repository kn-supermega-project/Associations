using System;
using System.Collections.Generic;
using System.Text;

namespace Associations.Common.UrlQueries
{
    public class PaginationUrlQuery
    {
        private const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;

        private int _pageCount = maxPageSize;
        public int PageSize
        {
            get { return _pageCount; }
            set { _pageCount = (value > maxPageSize) ? maxPageSize : value; }
        }
    }
}
