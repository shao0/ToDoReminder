using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using ToDoReminder.Client.Common.DialogServices.Proxy;
using ToDoReminder.Client.Common.Enums;
using ToDoReminder.Client.Common.Models;

namespace ToDoReminder.Client.ViewModels.Dialogs
{
    public class AddDialogViewModel : BindableBase, IDialogHostAware
    {
        public string DialogHostName { get; set; }

        #region string Title 标题
        /// <summary>
        /// 标题 字段
        /// </summary>
        private string _Title;
        /// <summary>
        /// 标题 属性
        /// </summary>
        public string Title
        {
            get => _Title;
            set
            {
                if (_Title != value)
                {
                    _Title = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region ModelEnum ModelType 类型
        /// <summary>
        /// 类型 字段
        /// </summary>
        private ModelType _ModelType;
        /// <summary>
        /// 类型 属性
        /// </summary>
        public ModelType ModelType
        {
            get => _ModelType;
            set
            {
                if (_ModelType != value)
                {
                    _ModelType = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region BaseModel Model 数据
        /// <summary>
        /// 数据 字段
        /// </summary>
        private MemoModel _Model;

        /// <summary>
        /// 数据 属性
        /// </summary>
        public MemoModel Model
        {
            get => _Model;
            set
            {
                if (_Model != value)
                {
                    _Model = value;
                    RaisePropertyChanged();
                }
            }
        }
        #endregion

        #region CancelCommand 取消命令
        /// <summary>
        /// 取消命令
        /// </summary>
        public DelegateCommand CancelCommand => new DelegateCommand(Cancel);

        private void Cancel()
        {
            if (DialogHost.IsDialogOpen(DialogHostName))
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.No));
        }

        #endregion

        #region SaveCommand 保存命令
        /// <summary>
        /// 保存命令
        /// </summary>
        public DelegateCommand SaveCommand => new DelegateCommand(Save);

        private void Save()
        {
            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                //确定时,把编辑的实体返回并且返回OK
                DialogParameters param = new DialogParameters
                {
                    { nameof(Model), Model },
                    {nameof(ModelType),ModelType },
                };
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.OK, param));
            }
        }

        #endregion

        public void OnDialogOpened(IDialogParameters parameters)
        {
            ModelType = parameters.GetValue<ModelType>("ModelType");
            Title = ModelType == ModelType.Memo ? "备忘录" : "待办事项";
            if (parameters.ContainsKey("Model"))
            {
                Model = parameters.GetValue<MemoModel>("Model");
            }
            else
            {
                Model = ModelType == ModelType.Memo ? new MemoModel() : new ToDoReminderModel() { ReminderDateTime = DateTime.Now };
            }

        }
    }
}
