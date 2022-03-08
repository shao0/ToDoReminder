using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoReminder.Share.DTO
{
    public class StatisticDTO
    {
        public string Title { get; set; }
        public int ToDoReminderCount { get; set; }
        public int ToDoReminderCompletedCount { get; set; }
        public int ToDoReminderInCompletedCount { get; set; }
        public int MemoCount { get; set; }
    }
}
