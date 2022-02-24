using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoReminder.Server.Entity;
using ToDoReminder.Server.Extensions;
using ToDoReminder.Server.Service.Proxy;
using ToDoReminder.Share;
using ToDoReminder.Share.DTO;

namespace ToDoReminder.Server.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public UserController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<ApiResponse> Login([FromQuery] string account, [FromQuery] string password)
        {
            try
            {
                IList<UserEntity>? list = await _service.GetAllAsync(u => u.Account == account && u.Password == password.ToMD5());
                if (list?.Count > 0)
                {
                    return new ApiResponse(_mapper.Map<UserDTO>(list.First()));
                }
                return new ApiResponse("登录失败!账号或密码错误");
            }
            catch (Exception e)
            {
                return new ApiResponse(e.Message);
            }
        }
        [HttpPost("[action]")]
        public async Task<ApiResponse> Register([FromBody] UserDTO dto)
        {
            try
            {
                UserEntity? entity = _mapper.Map<UserEntity>(dto);
                entity.Password = entity.Password.ToMD5();
                entity = await _service.AddAsync(entity);
                dto = _mapper.Map<UserDTO>(entity);
                return new ApiResponse(dto);
            }
            catch (Exception e)
            {
                return new ApiResponse(e.Message);
            }
        }


        [HttpGet("[action]")]
        public async Task<ApiResponse> ModifyPassword([FromQuery] string account, [FromQuery] string oldPasswrod, [FromQuery] string newPassword)
        {
            try
            {
                IList<UserEntity>? list = await _service.GetAllAsync(u => u.Account == account && u.Password == oldPasswrod.ToMD5());
                if (list?.Count > 0)
                {
                    UserEntity? entity = list.First();
                    entity.Password = newPassword.ToMD5();
                    entity = await _service.UpdateAsync(entity);

                    return new ApiResponse(_mapper.Map<UserDTO>(entity));
                }
                return new ApiResponse("账号或密码错误");
            }
            catch (Exception e)
            {
                return new ApiResponse(e.Message);
            }
        }
    }
}
