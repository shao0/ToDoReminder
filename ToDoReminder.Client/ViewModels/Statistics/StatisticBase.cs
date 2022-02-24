using AutoMapper;
using LiveCharts;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ToDoReminder.Client.Common.Helpers.Proxy;
using ToDoReminder.Client.Services.Proxy;

namespace ToDoReminder.Client.ViewModels.Statistics
{
    public class StatisticBase : BindableBase, INavigationAware
    {
        protected readonly IMapper mapper;
        protected readonly IStatisticService statisticService;
        protected readonly IEventAggregator ea;
        protected readonly ILog log;

        public SeriesCollection SeriesList { get; set; } = new SeriesCollection();


        public StatisticBase(IMapper mapper,
            IStatisticService statisticService,
            IEventAggregator ea,
            ILog log)
        {
            this.mapper = mapper;
            this.statisticService = statisticService;
            this.ea = ea;
            this.log = log;
        }


        #region LoadedCommand 加载命令
        /// <summary>
        /// 加载命令
        /// </summary>
        public ICommand LoadedCommand => new DelegateCommand(Loaded);

        protected virtual void Loaded() { }
        #endregion

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

    }
}
