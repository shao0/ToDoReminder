using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoReminder.Client.Services.Http;
using ToDoReminder.Client.Services.Proxy;
using ToDoReminder.Share.DTO;

namespace ToDoReminder.Client.Services
{
    public class UserService : BaseService<UserDTO>, IUserService
    {
        public UserService(HttpRestClient client) : base(client, "User")
        {
        }
    }
}
