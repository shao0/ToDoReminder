using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Windows.Input;
using System.Windows.Media;
using ToDoReminder.Client.Common.Extensions;

namespace ToDoReminder.Client.ViewModels.Settings
{
    public class SkinSettingViewModel : BindableBase
    {
        private readonly PaletteHelper palette;
        public IEnumerable<ISwatch> Swatches { get; private set; } = SwatchHelper.Swatches;


        public SkinSettingViewModel(PaletteHelper palette)
        {
            this.palette = palette;
        }

        #region bool DarkOrLight 主题明暗
        /// <summary>
        /// 主题明暗 字段
        /// </summary>
        private bool _DarkOrLight;

        /// <summary>
        /// 主题明暗 属性
        /// </summary>
        public bool DarkOrLight
        {
            get => _DarkOrLight;
            set
            {
                if (SetProperty(ref _DarkOrLight, value))
                {
                    palette.DefaultModifyTheme(value);
                }
            }
        }
        #endregion

        #region ChangedHueCommand 改变颜色命令
        /// <summary>
        /// 改变颜色命令
        /// </summary>
        public ICommand ChangedHueCommand => new DelegateCommand<object>(ChangedHue);

        private void ChangedHue(object obj)
        {
            palette.ModifyColor((Color)obj);
        }
        #endregion

        #region SaveThemeColorCommand 保存主题颜色命令
        /// <summary>
        /// 保存主题颜色命令
        /// </summary>
        public ICommand SaveThemeColorCommand => new DelegateCommand(SaveThemeColor);

        private void SaveThemeColor()
        {
            palette.SaveThemeColorToLocal(DarkOrLight);
        }

        #endregion



    }
}
