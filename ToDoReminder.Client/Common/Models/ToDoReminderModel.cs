using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoReminder.Client.Common.Models
{
    public class ToDoReminderModel: MemoModel
    {
        #region DateTime ReminderDateTime 提醒时间
        /// <summary>
        /// 提醒时间 字段
        /// </summary>
        private DateTime _ReminderDateTime;
        /// <summary>
        /// 提醒时间 属性
        /// </summary>
        public DateTime ReminderDateTime
        {
            get => _ReminderDateTime;
            set
            {
                if (_ReminderDateTime != value)
                {
                    _ReminderDateTime = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region int Status 状态
        /// <summary>
        /// 状态 字段
        /// </summary>
        private int _Status;
        /// <summary>
        /// 状态 属性
        /// </summary>
        public int Status
        {
            get => _Status;
            set
            {
                if (_Status != value)
                {
                    _Status = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

    }
}
