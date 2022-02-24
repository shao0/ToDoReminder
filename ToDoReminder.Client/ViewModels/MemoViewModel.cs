using AutoMapper;
using Prism.Commands;
using Prism.Events;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ToDoReminder.Client.Common.DialogServices.Proxy;
using ToDoReminder.Client.Common.Enums;
using ToDoReminder.Client.Common.Extensions;
using ToDoReminder.Client.Common.Helpers.Proxy;
using ToDoReminder.Client.Common.Models;
using ToDoReminder.Client.Services.Proxy;
using ToDoReminder.Share;
using ToDoReminder.Share.DTO;
using ToDoReminder.Share.Parameter;

namespace ToDoReminder.Client.ViewModels
{
    public class MemoViewModel : NavigationViewModel
    {
        protected readonly IMemoService service;
        protected readonly IMapper mapper;
        protected readonly IDialogHostService dialog;
        protected readonly IEventAggregator ea;
        protected readonly ILog log;
        protected readonly int SizePage = 28;

        public ObservableCollection<MemoModel> Memos { get; set; } = new ObservableCollection<MemoModel>();

        public MemoViewModel(IMemoService service, IMapper mapper, IDialogHostService dialog, IEventAggregator ea,ILog log)
        {
            this.service = service;
            this.mapper = mapper;
            this.dialog = dialog;
            this.ea = ea;
            this.log = log;
        }

        #region DateTime? StartDateTime 开始时段
        /// <summary>
        /// 开始时段 字段
        /// </summary>
        private DateTime? _StartDateTime;
        /// <summary>
        /// 开始时段 属性
        /// </summary>
        public DateTime? StartDateTime
        {
            get => _StartDateTime;
            set
            {
                if (_StartDateTime != value)
                {
                    _StartDateTime = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region DateTime? EndDateTime 结束时段
        /// <summary>
        /// 结束时段 字段
        /// </summary>
        private DateTime? _EndDateTime;
        /// <summary>
        /// 结束时段 属性
        /// </summary>
        public DateTime? EndDateTime
        {
            get => _EndDateTime;
            set
            {
                if (_EndDateTime != value)
                {
                    _EndDateTime = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region PageData PagedData 分页数据
        /// <summary>
        /// 分页数据 字段
        /// </summary>
        private PageData _PagedData;
        /// <summary>
        /// 分页数据 属性
        /// </summary>
        public PageData PagedData
        {
            get => _PagedData;
            set
            {
                if (_PagedData != value)
                {
                    _PagedData = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region string Search 搜索字段
        /// <summary>
        /// 搜索字段 字段
        /// </summary>
        private string _Search;

        /// <summary>
        /// 搜索字段 属性
        /// </summary>
        public string Search
        {
            get => _Search;
            set
            {
                if (_Search != value)
                {
                    _Search = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region QueryCommand 搜索命令
        /// <summary>
        /// 搜索命令
        /// </summary>
        public ICommand QueryCommand => new DelegateCommand(Query);

        protected virtual async void Query()
        {

            ea.Loading(true);
            var paramenr = new QueryParameter
            {
                IndexPage = PagedData.GoPage ?? 0,
                SizePage = PagedData.SizePage,
                Search = Search,
            };
            var apiResponse = await service.GetPagedListAsync(paramenr);
            if (apiResponse.Status)
            {
                Memos.Clear();
                foreach (var item in mapper.Map<List<MemoModel>>(apiResponse.Result.Items))
                {
                    Memos.Add(item);
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
        public ICommand AddedCommand => new DelegateCommand(Added);

        protected virtual async void Added()
        {
            DialogParameters param = new DialogParameters();
            param.Add("ModelType", ModelType.Memo);
            var dialogResult = await dialog.ShowDialog("AddDialogView", param);
            if (dialogResult.Result == ButtonResult.OK)
            {
                ea.Loading(true);
                var memo = mapper.Map<MemoDTO>(dialogResult.Parameters.GetValue<MemoModel>("Model"));
                var apiResponse = await service.AddAsync(memo);
                if (apiResponse.Status)
                {
                    var newModel = mapper.Map<MemoModel>(apiResponse.Result);
                    Memos.Add(newModel);
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
        public ICommand ModifyCommand => new DelegateCommand<object>(Modify);

        protected virtual async void Modify(object obj)
        {
            DialogParameters param = new DialogParameters();
            param.Add("Model", obj);
            param.Add("ModelType", ModelType.ToDoReminder);
            var dialogResult = await dialog.ShowDialog("AddDialogView", param);
            if (dialogResult.Result == ButtonResult.OK)
            {
                ea.Loading(true);
                var model = dialogResult.Parameters.GetValue<MemoModel>("Model");
                var dto = mapper.Map<MemoDTO>(model);
                var apiResponse = await service.UpdateAsync(dto);
                if (apiResponse.Status)
                {
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

        #endregion

        #region DeletedCommand 删除命令
        /// <summary>
        /// 删除命令
        /// </summary>
        public ICommand DeletedCommand => new DelegateCommand<object>(Deleted);

        protected virtual async void Deleted(object obj)
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

        protected void RefreshPagedData<T>(IPagedList<T> paged)
        {
            PagedData.Total = paged.TotalCount;
            PagedData.TotalPage = paged.TotalPages;
            PagedData.CurrentPage = paged.PageIndex + 1;
            PagedData.GoPage = null;
            PagedData.HasNextPage = paged.HasNextPage;
            PagedData.HasPreviousPage = paged.HasPreviousPage;
        }
        protected override void Loaded()
        {
            PagedData = new PageData() { SizePage = SizePage };
            Query();
        }


    }
}
