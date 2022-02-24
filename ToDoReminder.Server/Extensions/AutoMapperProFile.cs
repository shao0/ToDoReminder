using AutoMapper;
using ToDoReminder.Server.Entity;
using ToDoReminder.Share.DTO;

namespace ToDoReminder.Server.Extensions
{
    public class AutoMapperProFile:MapperConfigurationExpression
    {
        public AutoMapperProFile()
        {
            CreateMap<ToDoReminderEntity, ToDoReminderDTO>().ReverseMap();
            CreateMap<MemoEntity, MemoDTO>().ReverseMap();
            CreateMap<UserEntity, UserDTO>().ReverseMap();
        }
    }
}
