
using System;

namespace ToDoReminder.Share.DTO
{
    /// <summary>
    /// 代办事项
    /// </summary>
    public class ToDoReminderDTO : MemoDTO
    {
        /// <summary>
        /// 提醒时间
        /// </summary>
        public DateTime ReminderDateTime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

    }
}
