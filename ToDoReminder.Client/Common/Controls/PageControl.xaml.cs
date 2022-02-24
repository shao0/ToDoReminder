using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ToDoReminder.Client.Common.Models;

namespace ToDoReminder.Client.Common.Controls
{
    /// <summary>
    /// PageControl.xaml 的交互逻辑
    /// </summary>
    public partial class PageControl : UserControl
    {
        public PageControl()
        {
            InitializeComponent();
        }


        private void GoBack_OnClick(object sender, RoutedEventArgs e)
        {
            if (PageDataContext.HasPreviousPage)
            {
                PageDataContext.GoPage = PageDataContext.CurrentPage -= 2;
                GoCommand.Execute(PageDataContext);
            }
        }

        private void GoForward_OnClick(object sender, RoutedEventArgs e)
        {
            if (PageDataContext.HasNextPage)
            {
                PageDataContext.GoPage = PageDataContext.CurrentPage;
                GoCommand.Execute(PageDataContext);
            }
        }
        //private void Go_OnClick(object sender, RoutedEventArgs e)
        //{
        //    GoCommand.Execute(PageDataContext);
        //}
        public PageData PageDataContext
        {
            get { return (PageData)GetValue(PageDataContextProperty); }
            set { SetValue(PageDataContextProperty, value); }
        }
        /// <summary>
        /// 数据上下文
        /// </summary>
        public static readonly DependencyProperty PageDataContextProperty =
            DependencyProperty.Register("PageDataContext", typeof(PageData), typeof(PageControl), new PropertyMetadata(default(PageData)));

        public ICommand GoCommand
        {
            get { return (ICommand)GetValue(GoCommandProperty); }
            set { SetValue(GoCommandProperty, value); }
        }
        /// <summary>
        /// 跳转
        /// </summary>
        public static readonly DependencyProperty GoCommandProperty =
            DependencyProperty.Register("GoCommand", typeof(ICommand), typeof(PageControl), new PropertyMetadata(default(ICommand)));

    }
}
