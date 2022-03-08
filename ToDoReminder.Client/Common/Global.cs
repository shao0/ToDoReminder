using MaterialDesignColors;
using MaterialDesignThemes.Wpf;
using MaterialDesignColors.ColorManipulation;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using ToDoReminder.Client.Common.Extensions;
using ToDoReminder.Client.Common.Models;
using System.Windows.Media;
using System.IO;
using Newtonsoft.Json;
using ToDoReminder.Client.Common.Helpers;
using ToDoReminder.Client.Common.Helpers.Proxy;

namespace ToDoReminder.Client.Common
{
    public static class Global
    {
        /// <summary>
        /// 内容区域名称
        /// </summary>
        public static string ContentRegion => "ContentRegion";
        /// <summary>
        /// 设置区域名称
        /// </summary>
        public static string SettingRegion => "SettingRegion";
        /// <summary>
        /// 统计区域名称
        /// </summary>
        public static string StatisticsRegion => "StatisticsRegion";

        public static string[] StatusList { get; set; } = { "未完成", "完成" };

        public static Dictionary<string, int?> StatusCondition { get; set; } = new Dictionary<string, int?> {
            { "全部" ,null},
            { "未完成" ,0},
            { "完成",1 }
        };
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="palette"></param>
        /// <param name="timer"></param>
        public static void Initial(PaletteHelper palette, ITimerHelper timer)
        {
            palette.InitialThemeColor();
            timer.Start();
        }


        #region 当前用户
        /// <summary>
        /// 当前用户
        /// </summary>
        public static UserModel CurrentUser { get; private set; }
        /// <summary>
        /// 设置当前用户
        /// </summary>
        public static void SetCurrentUser(UserModel user) => CurrentUser = user ?? new UserModel { NickName = "远辰" };

        #endregion

        #region 默认图片
        /// <summary>
        /// 默认图片Uri
        /// </summary>
        public static string DefaultImageUri = "pack://application:,,,/ToDoReminder.Client;component/Common/Resources/Images/Default.png";

        private static BitmapImage defaultImage;
        /// <summary>
        /// 默认图片
        /// </summary>
        public static BitmapImage DefaultImage
        {
            get
            {
                if (defaultImage == null)
                {
                    defaultImage = DefaultImageUri.UriToBitmapImage();
                }
                return defaultImage;
            }
        }
        #endregion

        #region 托管
        /// <summary>
        /// 托管
        /// </summary>
        public static NotifyIcon Icon { get; private set; } = InitialNotifyIcon();

        public static NotifyIcon InitialNotifyIcon()
        {
            var icon = new NotifyIcon();
            icon.Text = "ToDoReminder";
            var stream = System.Windows.Application
                .GetResourceStream(new Uri("pack://application:,,,/ToDoReminder.Client;component/Common/Resources/Images/icon.ico",
                    UriKind.Absolute))?.Stream;
            if (stream != null)
                icon.Icon = new System.Drawing.Icon(stream);
            icon.ContextMenu = new ContextMenu();
            var closeMenu = new MenuItem();
            closeMenu.Text = "退出";
            icon.ContextMenu.MenuItems.Add(closeMenu);
            icon.Visible = true;
            icon.DoubleClick += (s, e) =>
            {
                if (System.Windows.Application.Current.MainWindow == null) return;
                System.Windows.Application.Current.MainWindow.Show();
                System.Windows.Application.Current.MainWindow.Activate();
            };
            closeMenu.Click += (s, e) =>
            {
                if (icon != null)
                {
                    icon.Visible = false;
                    icon.Dispose();
                    icon = null;
                }
                System.Windows.Application.Current.Shutdown();
            };
            return icon;
        }
        #endregion
    }
}
