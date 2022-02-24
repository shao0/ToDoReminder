using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoReminder.Server.Entity
{
    /// <summary>
    /// 代办事项
    /// </summary>
    [Table("todoreminder"),]
    public class ToDoReminderEntity : BaseEntity
    {
        /// <summary>
        /// 标题
        /// </summary>
        [Column("title")]
        public string Title { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [Column("description")]
        public string Description { get; set; }
        /// <summary>
        /// 提醒时间
        /// </summary>
        [Column("reminder_datetime")]
        public DateTime ReminderDateTime { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Column("status")]
        public int Status { get; set; }

    }
}
