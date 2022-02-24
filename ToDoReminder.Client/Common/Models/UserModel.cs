
using System;
using System.Windows.Media.Imaging;
using ToDoReminder.Client.Common;

namespace ToDoReminder.Client.Common.Models
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserModel: BaseModel
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
        /// <summary>
        /// 头像
        /// </summary>
        public BitmapImage UserIcon { get; set; } = Global.DefaultImage;

    }
}
