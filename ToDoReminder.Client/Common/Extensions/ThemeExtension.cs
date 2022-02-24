using MaterialDesignColors;
using MaterialDesignColors.ColorManipulation;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ToDoReminder.Client.Common.Extensions
{
    public static class ThemeExtension
    {
        private static readonly string ThemeColorPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ThemeColor.json");

        /// <summary>
        /// 设置颜色
        /// </summary>
        /// <param name="color"></param>
        public static void ModifyColor(this PaletteHelper palette, Color color)
        {
            ITheme theme = palette.GetTheme();
            theme.PrimaryLight = new ColorPair(color.Lighten());
            theme.PrimaryMid = new ColorPair(color);
            theme.PrimaryDark = new ColorPair(color.Darken());
            palette.SetTheme(theme);
        }

        /// <summary>
        /// 修改主题
        /// </summary>
        /// <param name="modificationAction"></param>
        public static void ModifyTheme(this PaletteHelper palette, Action<ITheme> modificationAction)
        {
            ITheme theme = palette.GetTheme();
            modificationAction?.Invoke(theme);
            palette.SetTheme(theme);
        }

        /// <summary>
        /// 默认修改主题
        /// </summary>
        /// <param name="darkOrLight"></param>
        public static void DefaultModifyTheme(this PaletteHelper palette, bool darkOrLight) => palette.ModifyTheme(theme => theme.SetBaseTheme(darkOrLight ? Theme.Dark : Theme.Light));

        /// <summary>
        /// 保存
        /// </summary>
        public static void SaveThemeColorToLocal(this PaletteHelper palette, bool darkOrLight)
        {
            var theme = palette.GetTheme();
            File.WriteAllText(ThemeColorPath, JsonConvert.SerializeObject((darkOrLight, theme.PrimaryMid)));
        }

        public static void InitialThemeColor(this PaletteHelper palette)
        {
            var themeColorString = File.ReadAllText(ThemeColorPath);
            var themeColor = JsonConvert.DeserializeObject<(bool, ColorPair)>(themeColorString);
            palette.DefaultModifyTheme(themeColor.Item1);
            palette.ModifyColor(themeColor.Item2.Color);
        }

    }
}
