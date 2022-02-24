using AutoMapper;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ToDoReminder.Client.Common;
using ToDoReminder.Client.Common.DialogServices.Proxy;
using ToDoReminder.Client.Common.Enums;
using ToDoReminder.Client.Common.Events;
using ToDoReminder.Client.Common.Extensions;
using ToDoReminder.Client.Common.Helpers.Proxy;
using ToDoReminder.Client.Common.Models;
using ToDoReminder.Client.Services.Proxy;
using ToDoReminder.Share.DTO;
using ToDoReminder.Share.Parameter;

namespace ToDoReminder.Client.ViewModels
{
    public class IndexViewModel : NavigationViewModel
    {

        #region 注入项
        private readonly IToDoReminderService toDoReminderService;
        private readonly IMemoService memoService;
        private readonly IMapper mapper;
        private readonly IDialogHostService dialogHost;
        private readonly IStatisticService statisticsService;
        private readonly IEventAggregator ea;
        private readonly ITimerHelper timer;
        private readonly ILog log;
        #endregion

        DateTime? ReminderTime;

        public TaskBar[] TaskBars { get; set; } = new TaskBar[]
        {
            new TaskBar() { Icon = "ClockFast", Title = "汇总", Color = "#FF0CA0FF", Target = "ToDoReminderView" },
            new TaskBar() { Icon = "ClockCheckOutline", Title = "已完成", Color = "#FF1ECA3A", Target = "ToDoReminderView" },
            new TaskBar() { Icon = "ChartLineVariant", Title = "完成比例", Color = "#FF02C6DC", Target = "StatisticView" },
            new TaskBar() { Icon = "PlaylistStar", Title = "备忘录", Color = "#FFFFA000", Target = "MemoView" },
        };

        #region 提醒事项
        private List<ToDoReminderModel> ObserveList = new List<ToDoReminderModel>();
        private List<ToDoReminderModel> Reminders = new List<ToDoReminderModel>();
        public ObservableCollection<ToDoReminderModel> ToDoReminders { get; set; } = new ObservableCollection<ToDoReminderModel>();
        #endregion

        public ObservableCollection<MemoModel> Memos { get; set; } = new ObservableCollection<MemoModel>();

        public IndexViewModel(
            IToDoReminderService toDoReminderService,
            IMemoService memoService,
            IMapper mapper,
            IDialogHostService dialogHost,
            IStatisticService statisticsService,
            IEventAggregator ea,
            ITimerHelper timer,
            ILog Log)
        {
            this.toDoReminderService = toDoReminderService;
            this.memoService = memoService;
            this.mapper = mapper;
            this.dialogHost = dialogHost;
            this.statisticsService = statisticsService;
            this.ea = ea;
            this.timer = timer;
            log = Log;
        }

        #region DateTime NowTime 当前时间
        /// <summary>
        /// 当前时间 字段
        /// </summary>
        private DateTime _NowTime;
        /// <summary>
        /// 当前时间 属性
        /// </summary>
        public DateTime NowTime
        {
            get => _NowTime;
            set
            {
                if (_NowTime != value)
                {
                    _NowTime = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region UserModel CurrentUser 当前用户
        /// <summary>
        /// 当前用户 字段
        /// </summary>
        private UserModel _CurrentUser;

        /// <summary>
        /// 当前用户 属性
        /// </summary>
        public UserModel CurrentUser
        {
            get => _CurrentUser;
            set
            {
                if (_CurrentUser != value)
                {
                    _CurrentUser = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region LoadedCommand 加载命令
        protected override async void Loaded()
        {
            CurrentUser = Global.CurrentUser;
            NowTime = DateTime.Now;
            ea.Loading(true);
            QueryToDoReminderList();
            QueryMemoList();
            await RefreshStatistics();
            InitalTimer();
            ea.Loading(false);
        }

        private async Task RefreshStatistics()
        {
            var response = await statisticsService.IndexDataAsync();
            if (response.Status)
            {
                var statistics = mapper.Map<StatisticModel>(response.Result);
                TaskBars[0].Count = statistics.ToDoReminderCount.ToString();
                TaskBars[1].Count = statistics.ToDoReminderCompletedCount.ToString();
                TaskBars[2].Count = statistics.ToDoReminderCompletedRatio.ToString("p");
                TaskBars[3].Count = statistics.MemoeCount.ToString();
            }
        }

        async void QueryToDoReminderList()
        {
            var apiResponse = await toDoReminderService.QueryPagedListAsync(new ToDoReminderParameter { IndexPage = 0, SizePage = 200, Status = 0 });
            if (apiResponse.Status)
            {
                ToDoReminders.Clear();
                Reminders.Clear();
                var nowTime = DateTime.Now;
                foreach (var item in mapper.Map<List<ToDoReminderModel>>(apiResponse.Result.Items))
                {
                    ToDoReminders.Add(item);
                    if (item.ReminderDateTime <= nowTime)
                    {
                        Reminders.Add(item);
                    }
                    else
                    {
                        ObserveList.Add(item);
                    }
                }
                ShowReminder();
            }
            else
            {
                log.WriteError(apiResponse.Message);
                ea.SendMessage($"失败:{apiResponse.Message}");
            }
        }

        async void QueryMemoList()
        {
            var apiResponse = await memoService.GetPagedListAsync(new QueryParameter { IndexPage = 0, SizePage = 200 });
            if (apiResponse.Status)
            {
                Memos.Clear();
                foreach (var item in mapper.Map<List<MemoModel>>(apiResponse.Result.Items))
                {
                    Memos.Add(item);
                }
            }
            else
            {
                log.WriteError(apiResponse.Message);
                ea.SendMessage($"失败:{apiResponse.Message}");
            }
        }
        #endregion

        #region NavigateCommand 导航命令
        /// <summary>
        /// 导航命令
        /// </summary>
        public ICommand NavigateCommand => new DelegateCommand<TaskBar>(Navigate);

        private void Navigate(TaskBar obj)
        {
            var parameter = new NavigationParameters();
            if (obj.Title == "已完成") parameter.Add("Status", 1);
            parameter.Add("ViewName", obj.Target);
            ea.GetEvent<NavigateEvent>().Publish(parameter);
        }

        #endregion

        #region AddedCommand 新增命令
        /// <summary>
        /// 新增命令
        /// </summary>
        public ICommand AddedCommand => new DelegateCommand<object>(Added);

        private async void Added(object obj)
        {
            if (obj is string s)
            {
                switch (s)
                {
                    case "TodoReminder":
                        await SaveModel(ModelType.ToDoReminder);
                        break;
                    case "Memo":
                        await
                            SaveModel(ModelType.Memo);
                        break;
                }
            }
        }

        #endregion

        #region ModifyCommand 修改命令
        /// <summary>
        /// 修改命令
        /// </summary>
        public ICommand ModifyCommand => new DelegateCommand<object>(Modify);

        private async void Modify(object obj)
        {
            if (obj is ToDoReminderModel ToDoReminder)
            {
                await SaveModel(ModelType.ToDoReminder, ToDoReminder);
            }
            else if (obj is MemoModel memo)
            {
                await SaveModel(ModelType.Memo, memo);
            }

        }

        #endregion

        #region ModifyStatusCommand 修改状态命令
        /// <summary>
        /// 修改状态命令
        /// </summary>
        public ICommand ModifyStatusCommand => new DelegateCommand<object>(ModifyStatus);

        private async void ModifyStatus(object obj)
        {
            if (obj is ToDoReminderModel ToDoReminder)
            {
                ea.Loading(true);
                await ToDoReminderSave(ToDoReminder);
                ea.Loading(false);
            }
        }

        #endregion

        private async Task SaveModel(ModelType modelType, MemoModel model = null)
        {
            DialogParameters param = new DialogParameters();
            if (model != null)
                param.Add("Model", model);
            param.Add("ModelType", modelType);
            var dialogResult = await dialogHost.ShowDialog("AddDialogView", param);
            if (dialogResult.Result == ButtonResult.OK)
            {
                ea.Loading(true);
                switch (dialogResult.Parameters.GetValue<ModelType>("ModelType"))
                {
                    case ModelType.Memo:
                        await MemoSave(dialogResult.Parameters.GetValue<MemoModel>("Model"));
                        break;
                    case ModelType.ToDoReminder:
                        await ToDoReminderSave(dialogResult.Parameters.GetValue<ToDoReminderModel>("Model"));
                        break;
                }
                ea.Loading(false);
            }
        }

        private async Task MemoSave(MemoModel model)
        {
            var memoDto = mapper.Map<MemoDTO>(model);
            var apiResponse = model.Id > 0 ? await memoService.UpdateAsync(memoDto) : await memoService.AddAsync(memoDto);
            if (apiResponse.Status)
            {
                var newModel = mapper.Map<MemoModel>(apiResponse.Result);
                if (model.Id == 0) Memos.Add(newModel);
                await RefreshStatistics();
            }
            else
            {
                log.WriteError(apiResponse.Message);
                ea.SendMessage($"失败:{apiResponse.Message}");
            }
        }

        private async Task ToDoReminderSave(ToDoReminderModel model)
        {
            var toDoReminderDto = mapper.Map<ToDoReminderDTO>(model);
            var apiResponse = model.Id > 0 ? await toDoReminderService.UpdateAsync(toDoReminderDto) : await toDoReminderService.AddAsync(toDoReminderDto);
            if (apiResponse.Status)
            {
                var newModel = mapper.Map<ToDoReminderModel>(apiResponse.Result);
                if (model.Id == 0)
                {
                    ToDoReminders.Add(newModel);
                    ObserveList.Add(newModel);
                }
                await RefreshStatistics();
            }
            else
            {
                log.WriteError(apiResponse.Message);
                ea.SendMessage($"失败:{apiResponse.Message}");
            }
        }

        void InitalTimer()
        {
            void Timer_Elapsed(object sender, EventArgs e)
            {
                NowTime = DateTime.Now;
                if (ReminderTime <= NowTime)
                {
                    ShowReminder();
                    return;
                }
                if (ObserveList?.Count > 0)
                {
                    var first = ObserveList.OrderBy(x => x.ReminderDateTime).First();
                    if (first.ReminderDateTime < NowTime)
                    {
                        ObserveList.Remove(first);
                        Reminders.Add(first);
                        ShowReminder();
                    }
                }
            }
            timer.Elapsed -= Timer_Elapsed;
            timer.Elapsed += Timer_Elapsed;
        }

        void ShowReminder()
        {
            ReminderTime = null;
            DialogParameters parameter = new DialogParameters { { "ModelList", Reminders }, };
            dialogHost.ShowEdgeWindow("ReminderView", parameter, async result =>
            {
                if (result.Result == ButtonResult.OK)
                {
                    var modelList = result.Parameters.GetValue<IEnumerable<ToDoReminderModel>>("ModelList");
                    foreach (var item in modelList)
                    {
                        await ToDoReminderSave(item);
                        Reminders.Remove(item);
                    }
                }
                if (Reminders.Count != 0)
                {
                    ReminderTime = NowTime.AddMinutes(30);
                }
            });
        }
    }
}
