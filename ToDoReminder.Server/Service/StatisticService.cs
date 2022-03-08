using ToDoReminder.Server.DBContext;
using ToDoReminder.Server.Entity;
using ToDoReminder.Server.Service.Proxy;
using ToDoReminder.Share.DTO;

namespace ToDoReminder.Server.Service
{
    public class StatisticService : IStatisticService
    {
        protected readonly ToDoReminderContext context;

        public StatisticService(ToDoReminderContext context)
        {
            this.context = context;
        }

        public async Task<StatisticDTO> IndexDataAsync()
        {
            return await Task.Run(() =>
            {
                var toDoReminderQueryable = context.Set<ToDoReminderEntity>();
                var memoCount = context.Set<MemoEntity>().Count();
                return new StatisticDTO()
                {
                    ToDoReminderCount = toDoReminderQueryable.Count(),
                    ToDoReminderCompletedCount = toDoReminderQueryable.Count(t => t.Status == 1),
                    ToDoReminderInCompletedCount = toDoReminderQueryable.Count(t => t.Status == 0),
                    MemoCount = memoCount,
                };
            });
        }

        public async Task<List<StatisticDTO>> MonthlyToDoReminderAsync()
        {
            return await Task.Run(() =>
            {
                var monthList = context.Set<ToDoReminderEntity>().GroupBy(t => t.ReminderDateTime.Month,t=>t.Status).Select(t => new StatisticDTO
                {
                    ToDoReminderInCompletedCount = t.Sum(s => s == 0 ? 1 : 0),
                    ToDoReminderCompletedCount = t.Sum(s => s == 1 ? 1 : 0),
                    Title = $"{t.Key}月",
                }).ToList();
                return monthList;
            });
        }
    }
}
