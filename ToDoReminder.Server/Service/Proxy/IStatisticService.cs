using ToDoReminder.Share.DTO;

namespace ToDoReminder.Server.Service.Proxy
{
    public interface IStatisticService
    {
        Task<StatisticDTO> IndexDataAsync();

        Task<List<StatisticDTO>> MonthlyToDoReminderAsync();

    }
}
