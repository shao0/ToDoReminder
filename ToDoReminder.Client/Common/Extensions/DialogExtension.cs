using Prism.Events;
using System;
using ToDoReminder.Client.Common.Events;

namespace ToDoReminder.Client.Common.Extensions
{
    public static class DialogExtensions
    {

        /// <summary>
        /// 推送等待消息
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="model"></param>
        public static void Loading(this IEventAggregator aggregator, bool isOpen)
        {
            aggregator.GetEvent<LoadingEvent>().Publish(new LoadingModel { IsOpen = isOpen });
        }

        /// <summary>
        /// 注册等待消息
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="action"></param>
        public static void ResgiterLoading(this IEventAggregator aggregator, Action<LoadingModel> action)
        {
            aggregator.GetEvent<LoadingEvent>().Subscribe(action);
        }

        /// <summary>
        /// 注册提示消息 
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="action"></param>
        public static void ResgiterMessage(this IEventAggregator aggregator,
            Action<MessageModel> action, string filterName = "Main")
        {
            aggregator.GetEvent<MessageEvent>().Subscribe(action,
                ThreadOption.PublisherThread, true, (m) =>
                {
                    return m.Filter.Equals(filterName);
                });
        }

        /// <summary>
        /// 发送提示消息
        /// </summary>
        /// <param name="aggregator"></param>
        /// <param name="message"></param>
        public static void SendMessage(this IEventAggregator aggregator, string message, string filterName = "Main")
        {
            aggregator.GetEvent<MessageEvent>().Publish(new MessageModel()
            {
                Filter = filterName,
                Message = message,
            });
        }
    }
}
