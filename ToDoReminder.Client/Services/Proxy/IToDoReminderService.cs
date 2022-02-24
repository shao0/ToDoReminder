using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoReminder.Share;
using ToDoReminder.Share.DTO;
using ToDoReminder.Share.Parameter;

namespace ToDoReminder.Client.Services.Proxy
{
    public interface IToDoReminderService : IBaseService<ToDoReminderDTO>
    {
        Task<ApiResponse<PagedList<ToDoReminderDTO>>> QueryPagedListAsync(ToDoReminderParameter parameter);
    }
}
