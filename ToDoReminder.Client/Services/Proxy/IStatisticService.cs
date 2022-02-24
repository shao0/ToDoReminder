using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoReminder.Share;
using ToDoReminder.Share.DTO;

namespace ToDoReminder.Client.Services.Proxy
{
    public interface IStatisticService
    {
        Task<ApiResponse<StatisticDTO>> IndexDataAsync();

        Task<ApiResponse<List<StatisticDTO>>> MonthlyToDoReminderAsync();
    }
}
