using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ToDoReminder.Client.Common.Enums;
using ToDoReminder.Client.Common.Helpers.Proxy;

namespace ToDoReminder.Client.Common.Helpers
{

    public class LogInfo
    {
        public LogType LogType { get; set; } = LogType.UnKnow;

        public string Described { get; set; }

        public DateTime InvokeTime { get; set; }

        public object Content { get; set; }
    }

    public class LogHelper : ILog
    {
        public string FolderPath { get; set; }

        private readonly object _locker;

        private readonly ConcurrentQueue<LogInfo> _logQueue;

        private bool _runWrite;

        public LogHelper()
        {
            FolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log");
            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }
            _locker = new object();
            _logQueue = new ConcurrentQueue<LogInfo>();
        }
        /// <summary>
        /// 获取类型描述
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string LogTypeString(LogType type)
        {
            var result = "未知信息";
            switch (type)
            {
                case LogType.Info:
                    result = "普通信息";
                    break;
                case LogType.Error:
                    result = "错误信息";
                    break;
                case LogType.Warning:
                    result = "警告信息";
                    break;
                case LogType.Exception:
                    result = "异常信息";
                    break;
                case LogType.UnKnow:
                default:
                    break;
            }
            return result;
        }
        /// <summary>
        /// 写入日志到文件中
        /// </summary>
        /// <param name="info">记录日志相关信息</param>
        private void WriteTask(LogInfo info)
        {
            var filePath = Path.Combine(FolderPath, $"{DateTime.Now:yyyy年MM月dd日}.log");
            var sb = new StringBuilder();
            sb.Append('\r');
            sb.Append('-', 32);
            sb.AppendLine($"\r时间：{info.InvokeTime}");
            sb.Append(info.Described);
            if (info.Content is Exception exc && !string.IsNullOrWhiteSpace(exc.Source))
            {
                sb.AppendLine($"来源：{exc.Source}");
            }
            sb.AppendLine($"类别：{LogTypeString(info.LogType)}");
            sb.AppendLine($"内容：{info.Content}");
            sb.Append('-', 32);
            sb.Append('\r');
            File.AppendAllText(filePath, sb.ToString(), Encoding.UTF8);
        }
        /// <summary>
        /// 循环写入日志到文件中
        /// </summary>
        private void RunWriteTask()
        {
            while (_runWrite)
            {
                if (_logQueue.TryDequeue(out LogInfo info)) WriteTask(info);
                if (_runWrite && _logQueue.Count == 0) _runWrite = false;
            }
        }


        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="content"></param>
        /// <param name="logType"></param>
        /// <param name="fileName"></param>
        /// <param name="row"></param>
        /// <param name="methodName"></param>
        private void Write(object content, LogType logType, string fileName, int row, string methodName)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"文件：{fileName}");
            sb.AppendLine($"方法：{methodName}");
            sb.AppendLine($"行：{row}");
            _logQueue.Enqueue(new LogInfo
            {
                LogType = logType,
                Described = sb.ToString(),
                InvokeTime = DateTime.Now,
                Content = content,
            });
            if (_runWrite) return;
            lock (_locker)
            {
                if (_runWrite || _logQueue.Count <= 0) return;
                _runWrite = true;
                Task.Run(RunWriteTask);
            }
        }

        /// <summary>
        /// 记录错误日志
        /// </summary>
        /// <param name="content"></param>
        /// <param name="fileName"></param>
        /// <param name="row"></param>
        /// <param name="methodName"></param>
        public void WriteError(
            object content,
            [CallerFilePath] string fileName = null, 
            [CallerLineNumber] int row = 0,
            [CallerMemberName] string methodName = null) => Write(content, LogType.Error, fileName, row, methodName);
        /// <summary>
        /// 记录异常日志
        /// </summary>
        /// <param name="content"></param>
        /// <param name="fileName"></param>
        /// <param name="row"></param>
        /// <param name="methodName"></param>
        public void WriteException(
            object content,
            [CallerFilePath] string fileName = null,
            [CallerLineNumber] int row = 0,
            [CallerMemberName] string methodName = null) => Write(content, LogType.Exception, fileName, row, methodName);
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="content"></param>
        /// <param name="fileName"></param>
        /// <param name="row"></param>
        /// <param name="methodName"></param>
        public void WriteInfo(
            object content,
            [CallerFilePath] string fileName = null,
            [CallerLineNumber] int row = 0,
            [CallerMemberName] string methodName = null) => Write(content, LogType.Info, fileName, row, methodName);

        /// <summary>
        /// 记录警告日志
        /// </summary>
        /// <param name="content"></param>
        /// <param name="fileName"></param>
        /// <param name="row"></param>
        /// <param name="methodName"></param>
        public void WriteWarning(
            object content,
            [CallerFilePath] string fileName = null,
            [CallerLineNumber] int row = 0,
            [CallerMemberName] string methodName = null) => Write(content, LogType.Warning, fileName, row, methodName);
    }
}
