using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoReminder.Client.Common.Models
{
    public class MemoModel:BaseModel
    {

        #region string Title 概要
        /// <summary>
        /// 概要 字段
        /// </summary>
        private string _Title;
        /// <summary>
        /// 概要 属性
        /// </summary>
        public string Title
        {
            get => _Title;
            set
            {
                if (_Title != value)
                {
                    _Title = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region string  Description 描述
        /// <summary>
        /// 描述 字段
        /// </summary>
        private string  _Description;
        /// <summary>
        /// 描述 属性
        /// </summary>
        public string  Description
        {
            get => _Description;
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

    }
}
