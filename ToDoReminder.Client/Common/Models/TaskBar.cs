using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoReminder.Client.Common.Models
{
    public class TaskBar : BindableBase
    {
        public string Icon { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }
        public string Target { get; set; }

        #region string Count 数量
        /// <summary>
        /// 数量 字段
        /// </summary>
        private string _Count;
        /// <summary>
        /// 数量 属性
        /// </summary>
        public string Count
        {
            get => _Count;
            set
            {
                if (_Count != value)
                {
                    _Count = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

    }
}
