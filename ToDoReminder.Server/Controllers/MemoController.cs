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
    public class MemoController : BaseController
    {
        private readonly IMemoService _service;
        private readonly IMapper _mapper;
        public MemoController(IMemoService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        [HttpGet("[action]")]
        public async Task<ApiResponse> QueryList([FromQuery] QueryParameter query)
        {
            try
            {
                var pagedList = await _service.GetPagedListAsync(
                    t => (string.IsNullOrWhiteSpace(query.Search)
                    || t.Title.Contains(query.Search)
                    || t.Description.Contains(query.Search))
                    && (query.Start == null
                    || t.CreateDateTiem > query.Start
                    && query.End == null
                    || t.CreateDateTiem < query.End)
                    , query.IndexPage
                    , query.SizePage);
                return new ApiResponse(pagedList.PagedListConverter(items => _mapper.Map<IList<MemoDTO>>(items)));
            }
            catch (Exception e)
            {
                return new ApiResponse(e.Message);
            }
        }
        [HttpPost("[action]")]
        public async Task<ApiResponse> Add([FromBody] MemoDTO dto)
        {
            try
            {
                var entity = _mapper.Map<MemoEntity>(dto);
                dto = _mapper.Map<MemoDTO>(await _service.AddAsync(entity));
                return new ApiResponse(dto);
            }
            catch (Exception e)
            {
                return new ApiResponse(e.Message);
            }
        }
        [HttpPost("[action]")]
        public async Task<ApiResponse> Update([FromBody] MemoDTO dto)
        {
            try
            {
                var entity = _mapper.Map<MemoEntity>(dto);
                dto = _mapper.Map<MemoDTO>(await _service.UpdateAsync(entity));
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
