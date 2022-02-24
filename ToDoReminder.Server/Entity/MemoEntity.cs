using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoReminder.Server.Entity
{
    [Table("memo")]
    public class MemoEntity: BaseEntity
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
    }
}
