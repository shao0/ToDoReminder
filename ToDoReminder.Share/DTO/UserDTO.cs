

namespace ToDoReminder.Share.DTO
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserDTO : BaseDTO
    {
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

    }
}
