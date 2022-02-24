using System;
using System.Collections.Generic;
using System.Text;

namespace ToDoReminder.Share.Parameter
{
    /// <summary>
    /// 查询参数
    /// </summary>
    public class QueryParameter
    {
        /// <summary>
        /// 第几页
        /// </summary>
        public int IndexPage { get; set; } = 0;
        /// <summary>
        /// 每页大小
        /// </summary>
        public int SizePage { get; set; } = 20;
        /// <summary>
        /// 时间段开始
        /// </summary>
        public DateTime? Start { get; set; }
        /// <summary>
        /// 时间段结束
        /// </summary>
        public DateTime? End { get; set; }
        /// <summary>
        /// 查询字段
        /// </summary>
        public string Search { get; set; }
    }
}
