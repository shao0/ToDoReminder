using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ToDoReminder.Client.Common.Enums;

namespace ToDoReminder.Client.Common.Helpers.Proxy
{
    public interface ILog
    {
        /// <summary>
        /// 日志保存文件夹路径
        /// </summary>
        string FolderPath { get; set; }
        /// <summary>
        /// 记录普通日志
        /// </summary>
        /// <param name="content"></param>
        void WriteInfo(
            object content,
            [CallerFilePath] string fileName = null,
            [CallerLineNumber] int row = 0,
            [CallerMemberName] string methodName = null);
        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="content"></param>
        void WriteError(
            object content,
            [CallerFilePath] string fileName = null,
            [CallerLineNumber] int row = 0,
            [CallerMemberName] string methodName = null);    
        /// <summary>
        /// 记录警告日志
        /// </summary>
        /// <param name="content"></param>
        void WriteWarning(
            object content,
            [CallerFilePath] string fileName = null,
            [CallerLineNumber] int row = 0,
            [CallerMemberName] string methodName = null);
        /// <summary>
        /// 记录异常日志
        /// </summary>
        /// <param name="content"></param>
        void WriteException(
            object content,
            [CallerFilePath] string fileName = null,
            [CallerLineNumber] int row = 0,
            [CallerMemberName] string methodName = null);



    }
}
