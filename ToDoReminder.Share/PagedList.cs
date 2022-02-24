using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToDoReminder.Share
{
    /// <summary>
    /// 分页
    /// </summary>
    public class PagedList<T>:IPagedList<T>
    {
        /// <summary>
        /// 起始页
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        ///总页数
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public IList<T> Items { get; set; }

        /// <summary>
        ///是否有前一页
        /// </summary>
        public bool HasPreviousPage => PageIndex  > 0;

        /// <summary>
        /// 是否有下一页
        /// </summary>
        public bool HasNextPage => PageIndex+ 1 < TotalPages;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="source">数据源</param>
        /// <param name="pageIndex">起始页</param>
        /// <param name="pageSize">页大小</param>
        public PagedList(IEnumerable<T> source, int pageIndex, int pageSize)
        {
            if (source is IQueryable<T> querable)
            {
                PageIndex = pageIndex;
                PageSize = pageSize;
                TotalCount = querable.Count();
                TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

                Items = querable.Skip(PageIndex * PageSize).Take(PageSize).ToList();
            }
            else
            {
                PageIndex = pageIndex;
                PageSize = pageSize;
                TotalCount = source.Count();
                TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

                Items = source.Skip(PageIndex * PageSize).Take(PageSize).ToList();
            }
        }

        public PagedList() => Items = new T[0];

       
    }

}
