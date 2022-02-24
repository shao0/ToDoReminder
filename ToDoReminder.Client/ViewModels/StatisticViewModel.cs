using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoReminder.Client.Common;
using ToDoReminder.Client.Common.Models;

namespace ToDoReminder.Client.ViewModels
{
    public class StatisticViewModel : NavigationViewModel
    {
        private readonly IRegionManager region;

        public MenuModel[] Menus { get; set; } = new MenuModel[]
        {
            new MenuModel() { Icon = "ChartPie", Title = "完成率", Navigate = "CompletionRateView" },
            new MenuModel() { Icon = "ChartBar", Title = "月份待办事项", Navigate = "MonthlyToDoReminderColumnView" },
        };

        public StatisticViewModel(IRegionManager region)
        {
            this.region = region;
        }


        protected override void Loaded()
        {
            CheckedMenu = Menus.First();
            base.Loaded();
        }

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
                }
            }
        }
        /// <summary>
        /// 导航
        /// </summary>
        void NavigateChanged()
        {
            region.Regions[Global.StatisticsRegion].RequestNavigate(CheckedMenu.Navigate);
        }
        #endregion
    }
}
