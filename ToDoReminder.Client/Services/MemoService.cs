using System;
using System.Threading.Tasks;
using ToDoReminder.Client.Services.Http;
using ToDoReminder.Client.Services.Proxy;
using ToDoReminder.Share;
using ToDoReminder.Share.DTO;
using ToDoReminder.Share.Parameter;

namespace ToDoReminder.Client.Services
{
    public class MemoService : BaseService<MemoDTO>, IMemoService
    {
        public MemoService(HttpRestClient client) : base(client, "Memo")
        {
        }
    }
}
