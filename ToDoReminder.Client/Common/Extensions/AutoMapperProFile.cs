using AutoMapper;
using AutoMapper.Configuration;
using ToDoReminder.Client.Common.Models;
using ToDoReminder.Share.DTO;

namespace ToDoReminder.Client.Extensions
{
    public class AutoMapperProFile:MapperConfigurationExpression
    {
        public AutoMapperProFile()
        {
            CreateMap<ToDoReminderDTO, ToDoReminderModel>().ReverseMap();
            CreateMap<StatisticDTO, StatisticModel>().ReverseMap();
            CreateMap<MemoDTO, MemoModel>().ReverseMap();
            CreateMap<UserDTO, UserModel>().ReverseMap();
        }
    }
}
