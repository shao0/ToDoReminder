using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoReminder.Share;
using ToDoReminder.Share.Parameter;

namespace ToDoReminder.Client.Services.Proxy
{
    public interface IBaseService<T> where T : class
    {

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="funcWhere"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<ApiResponse<PagedList<T>>> GetPagedListAsync(QueryParameter parameter);
        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiResponse<T>> GetSingleAsync(int id);
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ApiResponse<T>> AddAsync(T entity);
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ApiResponse<T>> UpdateAsync(T entity);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ApiResponse<int>> DeleteAsync(int id);
    }
}
