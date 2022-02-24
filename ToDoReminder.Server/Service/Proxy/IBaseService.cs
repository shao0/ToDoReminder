using System.Linq.Expressions;
using ToDoReminder.Share;
using ToDoReminder.Share.Parameter;

namespace ToDoReminder.Server.Service.Proxy
{
    /// <summary>
    /// 服务基接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseService<T> where T : class
    {
        /// <summary>
        /// 查询数据源
        /// </summary>
        /// <param name="funcWhere"></param>
        /// <returns></returns>
        Task<IQueryable<T>> QueryAsync(Expression<Func<T, bool>> funcWhere);
        /// <summary>
        /// 查询所以
        /// </summary>
        /// <param name="funcWhere">条件委托</param>
        /// <returns></returns>
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> funcWhere);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="funcWhere"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        Task<IPagedList<T>> GetPagedListAsync(Expression<Func<T, bool>> funcWhere, int pageIndex = 0, int pageSize=20);
        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetSingleAsync(int id);
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> AddAsync(T entity);
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> UpdateAsync(T entity);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(int id);

    }
}
