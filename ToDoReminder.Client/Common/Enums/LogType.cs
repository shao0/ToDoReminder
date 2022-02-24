using System.ComponentModel;

namespace ToDoReminder.Client.Common.Enums
{
    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// 普通信息
        /// </summary>
        [Description("普通")] Info,
        /// <summary>
        /// 错误记录
        /// </summary>
        [Description("错误")] Error,
        /// <summary>
        /// 警告
        /// </summary>
        [Description("警告")] Warning,
        /// <summary>
        /// 异常
        /// </summary>
        [Description("异常")] Exception,
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")] UnKnow
    }
}
