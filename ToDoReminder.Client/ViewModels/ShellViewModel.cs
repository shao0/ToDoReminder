using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ToDoReminder.Client.Common;
using ToDoReminder.Client.Common.Events;
using ToDoReminder.Client.Common.Models;

namespace ToDoReminder.Client.ViewModels
{
    public class ShellViewModel : BindableBase
    {

        Window View;
        private readonly IRegionManager region;
        private readonly IEventAggregator ea;
        private IRegionNavigationJournal journal;

        public ShellViewModel(IRegionManager region, IRegionNavigationJournal journal, IEventAggregator ea)
        {
            this.region = region;
            this.journal = journal;
            this.ea = ea;
        }

        public ObservableCollection<MenuModel> Menus { get; set; } = new ObservableCollection<MenuModel>
        {
            new MenuModel() { Icon = "Home", Title = "首页", Navigate = "IndexView" },
            new MenuModel() { Icon = "NotebookOutline", Title = "待办事项", Navigate = "ToDoReminderView" },
            new MenuModel() { Icon = "NotebookPlus", Title = "备忘录", Navigate = "MemoView" },
            new MenuModel() { Icon = "ChartLine", Title = "统计", Navigate = "StatisticView" },
            new MenuModel() { Icon = "Cog", Title = "设置", Navigate = "SettingView" },
        };

        #region bool OpenLeft 打开左边
        /// <summary>
        /// 打开左边 字段
        /// </summary>
        private bool _OpenLeft;
        /// <summary>
        /// 打开左边 属性
        /// </summary>
        public bool OpenLeft
        {
            get => _OpenLeft;
            set
            {
                if (_OpenLeft != value)
                {
                    _OpenLeft = value;
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

        #region MenuModel CheckedMenu 选中菜单
        /// <summary>
        /// 选中菜单 字段
        /// </summary>
        private MenuModel _CheckedMenu;
        /// <summary>
        /// 选中菜单 属性
        /// </summary>
        public MenuModel CheckedMenu
        {
            get => _CheckedMenu;
            set
            {
                if (_CheckedMenu != value)
                {
                    _CheckedMenu = value;
                    RaisePropertyChanged();
                    NavigateChanged();
                    OpenLeft = false;
                }
            }
        }
        /// <summary>
        /// 设置选择项
        /// </summary>
        /// <param name="viewName"></param>
        public void SetCheckedMenu(string viewName)
        {
            var menu = Menus.FirstOrDefault(m => m.Navigate == viewName);
            if (menu != null)
            {
                _CheckedMenu = menu;
                RaisePropertyChanged(nameof(CheckedMenu));
            }
        }
        /// <summary>
        /// 设置选择项
        /// </summary>
        /// <param name="parameter"></param>
        void SetCheckedMenu(NavigationParameters parameter)
        {
            var viewName = parameter.GetValue<string>("ViewName");
            SetCheckedMenu(viewName);
            NavigateChanged(parameter);
        }
        /// <summary>
        /// 导航
        /// </summary>
        /// <param name="parameter"></param>
        void NavigateChanged(NavigationParameters parameter = null)
        {
            region.Regions[Global.ContentRegion].RequestNavigate(CheckedMenu.Navigate, back =>
            {
                journal = back.Context.NavigationService.Journal;
            }, parameter);
        }
        #endregion

        #region LoadedCommand 加载命令
        /// <summary>
        /// 加载命令
        /// </summary>
        public ICommand LoadedCommand => new DelegateCommand(Loaded);

        private void Loaded()
        {
            Global.SetCurrentUser(null);
            CheckedMenu = Menus.First();
            CurrentUser = Global.CurrentUser;
            ea.GetEvent<NavigateEvent>().Subscribe(SetCheckedMenu);
        }
        #endregion

        #region HomeCommand 主页命令
        /// <summary>
        /// 主页命令
        /// </summary>
        public ICommand HomeCommand => new DelegateCommand(Home);

        private void Home()
        {
            SetCheckedMenu("IndexView");
            NavigateChanged();
        }

        #endregion

        #region GoBackCommand 返回命令
        /// <summary>
        /// 返回命令
        /// </summary>
        public DelegateCommand GoBackCommand => new DelegateCommand(GoBack, () => journal.CanGoBack).ObservesProperty(() => CheckedMenu);

        private void GoBack()
        {
            journal.GoBack();
            _CheckedMenu = Menus.FirstOrDefault(m => m.Navigate == journal.CurrentEntry.Uri.ToString());
            RaisePropertyChanged(nameof(CheckedMenu));
        }

        #endregion

        #region GoForwardCommand 前进命令
        /// <summary>
        /// 前进命令
        /// </summary>
        public DelegateCommand GoForwardCommand => new DelegateCommand(GoForward, () => journal.CanGoForward).ObservesProperty(() => CheckedMenu);

        private void GoForward()
        {
            journal.GoForward();
            _CheckedMenu = Menus.FirstOrDefault(m => m.Navigate == journal.CurrentEntry.Uri.ToString());
            RaisePropertyChanged(nameof(CheckedMenu));
        }

        #endregion

    }
}
