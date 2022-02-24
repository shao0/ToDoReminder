using Prism.Regions;
using System.Linq;
using ToDoReminder.Client.Common;
using ToDoReminder.Client.Common.Models;

namespace ToDoReminder.Client.ViewModels
{
    public class SettingViewModel : NavigationViewModel
    {
        private readonly IRegionManager region;

        public MenuModel[] Menus { get; set; } = new MenuModel[]
        {
            new MenuModel() { Icon = "Palette", Title = "个性化", Navigate = "SkinSettingView" },
            new MenuModel() { Icon = "Cog", Title = "系统设置", Navigate = "SystemSettingView" },
        };

        public SettingViewModel(IRegionManager region)
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
            region.Regions[Global.SettingRegion].RequestNavigate(CheckedMenu.Navigate);
        }
        #endregion
    }
}
