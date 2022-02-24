using Microsoft.AspNetCore.Mvc;
using ToDoReminder.Server.Service.Proxy;
using ToDoReminder.Share;
using ToDoReminder.Share.DTO;

namespace ToDoReminder.Server.Controllers
{
    public class StatisticController : BaseController
    {
        private readonly IStatisticService statisticService;

        public StatisticController(IStatisticService statisticService)
        {
            this.statisticService = statisticService;
        }


        [HttpGet("[action]")]
        public async Task<ApiResponse> IndexData()
        {
            try
            {
                var dto = await statisticService.IndexDataAsync(); ;
                return new ApiResponse(dto);
            }
            catch (Exception e)
            {
                return new ApiResponse(e.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<ApiResponse> MonthlyToDoReminder()
        {
            try
            {
               var dtoList = await statisticService.MonthlyToDoReminderAsync();
                return new ApiResponse(dtoList);
            }
            catch (Exception e)
            {
                return new ApiResponse(e.Message);
            }
        }
    }
}
