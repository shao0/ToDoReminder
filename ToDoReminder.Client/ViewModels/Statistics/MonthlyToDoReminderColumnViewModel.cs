using AutoMapper;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoReminder.Client.Common.Extensions;
using ToDoReminder.Client.Common.Helpers.Proxy;
using ToDoReminder.Client.Common.Models;
using ToDoReminder.Client.Services.Proxy;

namespace ToDoReminder.Client.ViewModels.Statistics
{
    internal class MonthlyToDoReminderColumnViewModel : StatisticBase
    {
        public ObservableCollection<string> Names { get; set; } = new ObservableCollection<string>();
        public MonthlyToDoReminderColumnViewModel(IMapper mapper, IStatisticService statisticService, IEventAggregator ea, ILog log) : base(mapper, statisticService, ea, log)
        {
            SeriesList.Add(new ColumnSeries
            {
                Title = "完成数",
                Values = new ChartValues<int>(),
                DataLabels = true,
            });
            SeriesList.Add(new ColumnSeries
            {
                Title = "未完成",
                Values = new ChartValues<int>(),
                DataLabels = true,
            });
        }
        protected override async void Loaded()
        {
            await Query();
        }

        private async Task Query()
        {
            var apiResponse = await statisticService.MonthlyToDoReminderAsync();
            if (apiResponse.Status)
            {
                if (SeriesList[0].Values is ChartValues<int> value0
                    && SeriesList[1].Values is ChartValues<int> value1)
                {
                    value0.Clear();
                    value1.Clear();
                    Names.Clear();
                    var statisticList = mapper.Map<List<StatisticModel>>(apiResponse.Result);
                    foreach (var item in statisticList)
                    {
                        value0.Add(item.ToDoReminderCompletedCount);
                        value1.Add(item.ToDoReminderInCompletedCount);
                        Names.Add(item.Title);
                    }

                }
                ea.SendMessage("新增完成");
            }
            else
            {
                log.WriteError(apiResponse.Message);
                ea.SendMessage($"失败:{apiResponse.Message}");
            }
        }
    }
}
