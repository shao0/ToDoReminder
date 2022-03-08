using AutoMapper;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToDoReminder.Client.Common.Extensions;
using ToDoReminder.Client.Common.Helpers.Proxy;
using ToDoReminder.Client.Common.Models;
using ToDoReminder.Client.Services.Proxy;

namespace ToDoReminder.Client.ViewModels.Statistics
{
    public class CompletionRateViewModel : StatisticBase
    {
        public CompletionRateViewModel(IMapper mapper, IStatisticService statisticService, IEventAggregator ea, ILog log) 
            : base(mapper, statisticService, ea, log)
        {
            string PointLabel(ChartPoint charPoint) => $"{charPoint.Y}({charPoint.Participation:p})";
            SeriesList.Add(new PieSeries
            {
                Title = "完成数",
                Values = new ChartValues<ObservableValue> { new ObservableValue(0) },
                DataLabels = true,
                LabelPoint = PointLabel,
            }); SeriesList.Add(new PieSeries
            {
                Title = "未完成数",
                Values = new ChartValues<ObservableValue> { new ObservableValue(0) },
                DataLabels = true,
                LabelPoint = PointLabel,
            });
        }

        #region LoadedCommand 加载命令
        /// <summary>
        /// 加载命令
        /// </summary>

        protected override async void Loaded()
        {
            await Query();
        }

        private async Task Query()
        {
            var apiResponse = await statisticService.IndexDataAsync();
            if (apiResponse.Status)
            {
                var statistic = mapper.Map<StatisticModel>(apiResponse.Result);
                if (SeriesList[0].Values[0] is ObservableValue v1 && SeriesList[1].Values[0] is ObservableValue v2)
                {
                    v1.Value = statistic.ToDoReminderCompletedCount;
                    v2.Value = statistic.ToDoReminderCount - statistic.ToDoReminderCompletedCount;
                }
            }
            else
            {
                log.WriteError(apiResponse.Message);
                ea.SendMessage($"失败:{apiResponse.Message}");
            }
        }
        #endregion

    }
}
