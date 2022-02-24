using ToDoReminder.Server.DBContext;
using ToDoReminder.Server.Entity;
using ToDoReminder.Server.Service.Proxy;

namespace ToDoReminder.Server.Service
{
    public class ToDoReminderService : BaseService<ToDoReminderEntity>, IToDoReminderService
    {
        public ToDoReminderService(ToDoReminderContext context) : base(context)
        {
        }
    }
}
