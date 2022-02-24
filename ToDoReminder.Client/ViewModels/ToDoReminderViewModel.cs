using AutoMapper;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using ToDoReminder.Client.Common.DialogServices.Proxy;
using ToDoReminder.Client.Common.Enums;
using ToDoReminder.Client.Common.Extensions;
using ToDoReminder.Client.Common.Helpers.Proxy;
using ToDoReminder.Client.Common.Models;
using ToDoReminder.Client.Services.Proxy;
using ToDoReminder.Share.DTO;
using ToDoReminder.Share.Parameter;

namespace ToDoReminder.Client.ViewModels
{
    public class ToDoReminderViewModel : MemoViewModel
    {
        private new readonly IToDoReminderService service;

        public ObservableCollection<ToDoReminderModel> ToDoReminders { get; set; } = new ObservableCollection<ToDoReminderModel>();

        public ToDoReminderViewModel(IToDoReminderService service, IMapper mapper, IDialogHostService dialog, IEventAggregator ea,ILog log)
            : base(null, mapper, dialog, ea, log)
        {
            this.service = service;
        }


        #region int? SearchStatus 状态
        /// <summary>
        /// 状态 字段
        /// </summary>
        private int? _SearchStatus;
        /// <summary>
        /// 状态 属性
        /// </summary>
        public int? SearchStatus
        {
            get => _SearchStatus;
            set
            {
                if (_SearchStatus != value)
                {
                    _SearchStatus = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region QueryCommand 搜索命令
        /// <summary>
        /// 搜索命令
        /// </summary>

        protected override async void Query()
        {
            ea.Loading(true);
            var paramenr = new ToDoReminderParameter
            {
                IndexPage = PagedData.GoPage ?? 0,
                SizePage = PagedData.SizePage,
                Search = Search,
                Start = StartDateTime,
                End = EndDateTime,
                Status = SearchStatus,
            };
            var apiResponse = await service.QueryPagedListAsync(paramenr);
            if (apiResponse.Status)
            {
                ToDoReminders.Clear();
                foreach (var item in mapper.Map<List<ToDoReminderModel>>(apiResponse.Result.Items))
                {
                    ToDoReminders.Add(item);
                }
                RefreshPagedData(apiResponse.Result);
            }
            else
            {
                log.WriteError(apiResponse.Message);
                ea.SendMessage($"失败:{apiResponse.Message}");
            }
            ea.Loading(false);
        }

        #endregion

        #region AddedCommand 新增命令
        /// <summary>
        /// 新增命令
        /// </summary>
        protected override async void Added()
        {
            DialogParameters param = new DialogParameters();
            param.Add("ModelType", ModelType.ToDoReminder);
            var dialogResult = await dialog.ShowDialog("AddDialogView", param);
            if (dialogResult.Result == ButtonResult.OK)
            {
                ea.Loading(true);
                var toDoReminderDto = mapper.Map<ToDoReminderDTO>(dialogResult.Parameters.GetValue<ToDoReminderModel>("Model"));
                var apiResponse = await service.AddAsync(toDoReminderDto);
                if (apiResponse.Status)
                {
                    var newModel = mapper.Map<ToDoReminderModel>(apiResponse.Result);
                    ToDoReminders.Add(newModel);
                    ea.SendMessage("新增完成");
                }
                else
                {
                    log.WriteError(apiResponse.Message);
                    ea.SendMessage($"失败:{apiResponse.Message}");
                }
                ea.Loading(false);
            }
        }

        #endregion

        #region ModifyCommand 修改命令
        /// <summary>
        /// 修改命令
        /// </summary>
        protected override async void Modify(object obj)
        {
            DialogParameters param = new DialogParameters();
            param.Add("Model", obj);
            param.Add("ModelType", ModelType.ToDoReminder);
            var dialogResult = await dialog.ShowDialog("AddDialogView", param);
            if (dialogResult.Result == ButtonResult.OK)
            {
                var model = dialogResult.Parameters.GetValue<ToDoReminderModel>("Model");
                await UpdateSave(model);
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
            if (obj is ToDoReminderModel model)
            {
                await UpdateSave(model);
            }
        }

        #endregion

        #region DeletedCommand 删除命令
        /// <summary>
        /// 删除命令
        /// </summary>
        protected override async void Deleted(object obj)
        {
            if (obj is BaseModel model)
            {
                ea.Loading(true);
                var apiResponse = await service.DeleteAsync(model.Id);
                if (apiResponse.Status)
                {
                    ea.SendMessage("删除完成");
                }
                else
                {
                    log.WriteError(apiResponse.Message);
                    ea.SendMessage($"失败:{apiResponse.Message}");
                }
                ea.Loading(false);
            }
        }

        #endregion

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            if (navigationContext.Parameters.ContainsKey("Status"))
            {
                SearchStatus = navigationContext.Parameters.GetValue<int>("Status");
            }
        }

        async Task UpdateSave(ToDoReminderModel model)
        {
            ea.Loading(true);
            var toDoReminderDto = mapper.Map<ToDoReminderDTO>(model);
            var apiResponse = await service.UpdateAsync(toDoReminderDto);
            if (apiResponse.Status)
            {
                if (SearchStatus != null && model.Status != SearchStatus)
                {
                    ToDoReminders.Remove(model);
                }
                ea.SendMessage("修改完成");
            }
            else
            {
                log.WriteError(apiResponse.Message);
                ea.SendMessage($"失败:{apiResponse.Message}");
            }
            ea.Loading(false);
        }

    }
}
