
using System;

namespace ToDoReminder.Share.DTO
{
    public class BaseDTO
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDateTiem { get; set; } 
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateDateTime { get; set; } 
    }
}
