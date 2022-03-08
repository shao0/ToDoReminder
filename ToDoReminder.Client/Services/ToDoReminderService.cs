using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoReminder.Client.Services.Http;
using ToDoReminder.Client.Services.Proxy;
using ToDoReminder.Share;
using ToDoReminder.Share.DTO;
using ToDoReminder.Share.Parameter;

namespace ToDoReminder.Client.Services
{
    public class ToDoReminderService : BaseService<ToDoReminderDTO>, IToDoReminderService
    {
        public ToDoReminderService(HttpRestClient client) : base(client, "ToDoReminder")
        {
        }

        public async Task<ApiResponse<PagedList<ToDoReminderDTO>>> QueryPagedListAsync(ToDoReminderParameter parameter)
        {
            var request = new BaseRequest();
            request.Method = RestSharp.Method.GET;
            request.Route = $"{serviceName}/QueryList?IndexPage={parameter.IndexPage}&SizePage={parameter.SizePage}&Search={parameter.Search}&Status={parameter.Status}";
            return await client.ExecuteAsync<PagedList<ToDoReminderDTO>>(request);
        }
    }
}
