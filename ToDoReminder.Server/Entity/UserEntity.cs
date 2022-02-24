using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoReminder.Server.Entity
{
    /// <summary>
    /// 用户
    /// </summary>
    [Table("user")]
    public class UserEntity : BaseEntity
    {
        /// <summary>
        /// 昵称
        /// </summary>
        [Column("nickname")]
        public string NickName { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        [Column("account")]
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Column("password")]
        public string Password { get; set; }

    }
}
