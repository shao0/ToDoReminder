using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoReminder.Share.Parameter
{
    /// <summary>
    /// 代办提醒参数
    /// </summary>
    public class ToDoReminderParameter : QueryParameter
    {
        public int? Status { get; set; }
    }
}
