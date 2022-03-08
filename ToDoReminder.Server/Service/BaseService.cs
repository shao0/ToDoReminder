using System.Linq.Expressions;
using ToDoReminder.Server.DBContext;
using ToDoReminder.Server.Service.Proxy;
using ToDoReminder.Share;
using Microsoft.EntityFrameworkCore;
using ToDoReminder.Server.Extensions;
using ToDoReminder.Server.Entity;

namespace ToDoReminder.Server.Service
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        protected readonly ToDoReminderContext _context;

        public BaseService(ToDoReminderContext context)
        {
            _context = context;
        }
        public async Task<T> AddAsync(T entity)
        {
            if (entity == null) throw new Exception("新增数据为空");
            _context.Set<T>().Add(entity);
            await CommitAsync();
            return entity;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var entity = await FindAsync(id);
            _context.Set<T>().Remove(entity);
            return await CommitAsync();
        }

        public Task<IQueryable<T>> QueryAsync(Expression<Func<T, bool>> funcWhere)
        {
            return Task.Run(() => _context.Set<T>().Where(funcWhere));
        }
        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> funcWhere)
        {
            return await _context.Set<T>().Where(funcWhere).ToListAsync();
        }

        public async Task<IPagedList<T>> GetPagedListAsync(Expression<Func<T, bool>> funcWhere, int pageIndex = 0, int pageSize = 20)
        {
            var list = _context.Set<T>().Where(funcWhere).OrderBy(t => t.CreateDateTime);
            return await list.ToPagedListAsync(pageIndex, pageSize);
        }
        public async Task<T> GetSingleAsync(int id)
        {
            return await FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            if (entity == null) throw new Exception("更新数据为空");
            _context.Set<T>().Attach(entity);
            _context.Set<T>().Update(entity);
            if (await CommitAsync() == 0) throw new Exception("更新失败");
            return entity;
        }

        public async Task<T> FindAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null) throw new Exception("删除数据为空");
            return entity;
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public virtual void Dispose()
        {
            _context?.Dispose();
        }

    }
}
