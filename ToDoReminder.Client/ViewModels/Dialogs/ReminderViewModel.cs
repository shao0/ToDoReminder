using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ToDoReminder.Client.Common.Helpers.Proxy;
using ToDoReminder.Client.Common.Models;

namespace ToDoReminder.Client.ViewModels.Dialogs
{
    public class ReminderViewModel : BindableBase, IDialogAware
    {
        private readonly ITimerHelper timer;
        private readonly int closeSecondNumber = 30;
        int secondNumber = 0;

        FrameworkElement View;
        bool IsMouseOver => View != null && View.IsMouseOver;

        public string Title => "待办事项提醒";


        public event Action<IDialogResult> RequestClose;

        public ObservableCollection<ToDoReminderModel> Models { get; set; } = new ObservableCollection<ToDoReminderModel>();

        public ReminderViewModel(ITimerHelper timer)
        {
            this.timer = timer;
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        #region LoadedCommand 加载命令
        /// <summary>
        /// 加载命令
        /// </summary>
        public ICommand LoadedCommand => new DelegateCommand<FrameworkElement>(Loaded);

        private void Loaded(FrameworkElement v)
        {
            View = v;
            TimingClose();
        }

        #endregion

        #region ClosedCommand 关闭命令
        /// <summary>
        /// 关闭命令
        /// </summary>
        public ICommand ClosedCommand => new DelegateCommand<string>(Closed);

        private void Closed(string s)
        {
            switch (s)
            {
                case "Define":
                    RequestClose?.Invoke(new DialogResult(ButtonResult.OK, new DialogParameters { { "ModelList", Models.Where(t => t.Status == 1) } }));
                    break;
                case "Cancel":
                default:
                    RequestClose?.Invoke(new DialogResult(ButtonResult.Cancel));
                    break;
            }
        }

        #endregion

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("ModelList"))
            {
                Models.Clear();
                foreach (var item in parameters.GetValue<IEnumerable<ToDoReminderModel>>("ModelList"))
                {
                    Models.Add(item);
                }
            }
        }

        void TimingClose()
        {
            secondNumber = 0;
            timer.Elapsed -= Timer_Elapsed;
            timer.Elapsed += Timer_Elapsed;
            void Timer_Elapsed(object sender, EventArgs e)
            {
                if (secondNumber >= closeSecondNumber)
                {
                    timer.Elapsed -= Timer_Elapsed;
                    Closed("Cancel");
                }
                else
                {
                    if (!IsMouseOver) secondNumber++;
                }
            }
        }

    }
}
