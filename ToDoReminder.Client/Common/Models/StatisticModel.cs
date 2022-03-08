using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoReminder.Client.Common.Models
{
    public class StatisticModel
    {
        public string Title { get; set; }
        public int ToDoReminderCount { get; set; }
        public int ToDoReminderCompletedCount { get; set; }
        public int ToDoReminderInCompletedCount { get; set; }
        public int MemoCount { get; set; }
        public double ToDoReminderCompletedRatio => ToDoReminderCompletedCount * 1d / ToDoReminderCount ;
    }
}
