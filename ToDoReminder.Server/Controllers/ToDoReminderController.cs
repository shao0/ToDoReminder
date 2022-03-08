using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoReminder.Server.Entity;
using ToDoReminder.Server.Extensions;
using ToDoReminder.Server.Service.Proxy;
using ToDoReminder.Share;
using ToDoReminder.Share.DTO;
using ToDoReminder.Share.Parameter;

namespace ToDoReminder.Server.Controllers
{
    public class ToDoReminderController : BaseController
    {
        private readonly IToDoReminderService _service;
        private readonly IMapper _mapper;

        public ToDoReminderController(IToDoReminderService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        [HttpGet("[action]")]
        public async Task<ApiResponse> QueryList([FromQuery] ToDoReminderParameter query)
        {
            try
            {
                var pagedList = await _service.GetPagedListAsync(t =>
                (query.Status == null 
                || t.Status == query.Status)
                && (query.Start == null
                || t.CreateDateTime > query.Start
                && query.End == null
                || t.CreateDateTime < query.End)
                && (string.IsNullOrWhiteSpace(query.Search)
                || t.Title.Contains(query.Search)
                || t.Description.Contains(query.Search)
                ), query.IndexPage, query.SizePage);
                return new ApiResponse(pagedList.PagedListConverter(items => _mapper.Map<IList<ToDoReminderDTO>>(items)));
            }
            catch (Exception e)
            {
                return new ApiResponse(e.Message);
            }
        }
        [HttpPost("[action]")]
        public async Task<ApiResponse> Add([FromBody] ToDoReminderDTO dto)
        {
            try
            {
                var entity = _mapper.Map<ToDoReminderEntity>(dto);
                dto = _mapper.Map<ToDoReminderDTO>(await _service.AddAsync(entity));
                return new ApiResponse(dto);
            }
            catch (Exception e)
            {
                return new ApiResponse(e.Message);
            }
        }
        [HttpPost("[action]")]
        public async Task<ApiResponse> Update([FromBody] ToDoReminderDTO dto)
        {
            try
            {
                var entity = _mapper.Map<ToDoReminderEntity>(dto);
                dto = _mapper.Map<ToDoReminderDTO>(await _service.UpdateAsync(entity));
                return new ApiResponse(dto);
            }
            catch (Exception e)
            {
                return new ApiResponse(e.Message);
            }
        }
        [HttpDelete("[action]")]
        public async Task<ApiResponse> Delete([FromQuery] int id)
        {
            try
            {
                return new ApiResponse(await _service.DeleteAsync(id));
            }

            catch (Exception e)
            {
                return new ApiResponse(e.Message);
            }
        }
    }
}
