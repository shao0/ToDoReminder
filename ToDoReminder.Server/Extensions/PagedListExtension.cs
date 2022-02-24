using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDoReminder.Share;

namespace ToDoReminder.Server.Extensions
{
    public static class PagedListExtension
    {
        /// <summary>
        /// 转换分页集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static async Task<IPagedList<T>> ToPagedListAsync<T>( this IQueryable<T> source, int pageIndex, int pageSize)
        {
            //数据源大小
            var count = await source.CountAsync();

            var items = await source.Skip(pageIndex * pageSize)
                                    .Take(pageSize).ToListAsync().ConfigureAwait(false);
            return new PagedList<T>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = count,
                Items = items,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };
        }
        /// <summary>
        /// 分页转换器
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="converter"></param>
        /// <returns></returns>
        public static  IPagedList<TResult> PagedListConverter<TSource, TResult>(this IPagedList<TSource> source, Func<IList<TSource>, IList<TResult>> converter) => new PagedList<TResult>()
        {
            PageIndex = source.PageIndex,
            PageSize = source.PageSize,
            TotalCount = source.TotalCount,
            TotalPages = source.TotalPages,
            Items = converter(source.Items),
        };
    }
}
