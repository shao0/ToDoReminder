using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoReminder.Client.Services.Http;
using ToDoReminder.Client.Services.Proxy;
using ToDoReminder.Share;
using ToDoReminder.Share.DTO;

namespace ToDoReminder.Client.Services
{
    public class StatisticService : IStatisticService
    {

        protected readonly HttpRestClient client;

        public StatisticService(HttpRestClient client)
        {
            this.client = client;
        }
        public async Task<ApiResponse<StatisticDTO>> IndexDataAsync()
        {
            var request = new BaseRequest();
            request.Method = RestSharp.Method.GET;
            request.Route = $"Statistic/IndexData";
            return await client.ExecuteAsync<StatisticDTO>(request);
        }

        public async Task<ApiResponse<List<StatisticDTO>>> MonthlyToDoReminderAsync()
        {
            var request = new BaseRequest();
            request.Method = RestSharp.Method.GET;
            request.Route = $"Statistic/MonthlyToDoReminder";
            return await client.ExecuteAsync<List<StatisticDTO>>(request);
        }
    }
}
