using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoReminder.Server.Entity
{
    public class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("create_datetime")]
        public DateTime CreateDateTiem { get; set; } 

        [Column("update_datetime")]
        public DateTime UpdateDateTime { get; set; } 
    }
}
