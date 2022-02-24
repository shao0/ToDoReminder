using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoReminder.Share
{
    /// <summary>
    /// 分页
    /// </summary>
    public interface IPagedList<T>
    {
        /// <summary>
        /// 起始页
        /// </summary>
         int PageIndex { get; }
        /// <summary>
        /// 页大小
        /// </summary>
         int PageSize { get; }
        /// <summary>
        /// 总数
        /// </summary>
         int TotalCount { get; }
        /// <summary>
        ///总页数
        /// </summary>
         int TotalPages { get; }

        /// <summary>
        /// 数据
        /// </summary>
         IList<T> Items { get;}

        /// <summary>
        ///是否有前一页
        /// </summary>
         bool HasPreviousPage { get; }

        /// <summary>
        /// 是否有下一页
        /// </summary>
         bool HasNextPage { get; }

    }
}
