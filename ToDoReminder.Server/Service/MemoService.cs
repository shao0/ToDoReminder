using ToDoReminder.Server.DBContext;
using ToDoReminder.Server.Entity;
using ToDoReminder.Server.Service.Proxy;

namespace ToDoReminder.Server.Service
{
    public class MemoService : BaseService<MemoEntity>, IMemoService
    {
        public MemoService(ToDoReminderContext context) : base(context)
        {
        }
    }
}
