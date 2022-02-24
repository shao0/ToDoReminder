using ToDoReminder.Server.DBContext;
using ToDoReminder.Server.Entity;
using ToDoReminder.Server.Service.Proxy;

namespace ToDoReminder.Server.Service
{
    public class UserService : BaseService<UserEntity>, IUserService
    {
        public UserService(ToDoReminderContext context) : base(context)
        {
        }
    }
}
