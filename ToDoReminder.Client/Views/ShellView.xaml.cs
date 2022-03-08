using Prism.Events;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using ToDoReminder.Client.Common.Extensions;

namespace ToDoReminder.Client.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ShellView : Window
    {
        private readonly IEventAggregator aggregator;

        public ShellView(IEventAggregator aggregator)
        {
            InitializeComponent();

            this.aggregator = aggregator;
            Loaded += ShellView_Loaded;
            Closing += ShellView_Closing;
            Closed += ShellView_Closed;
        }

        private void ShellView_Closed(object sender, EventArgs e)
        {
            Header.MouseMove -= Header_MouseMove;
            Closing -= ShellView_Closing;
            Closed -= ShellView_Closed;
        }

        private void ShellView_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void ShellView_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded -= ShellView_Loaded;
            Header.MouseMove += Header_MouseMove;

            aggregator.RegisterMessage(arg =>
            {
                MainSnackbar.MessageQueue.Enqueue(arg.Message);
            });
            aggregator.RegisterLoading(arg =>
            {
                DialogHostControl.IsOpen = arg.IsOpen;

                if (DialogHostControl.IsOpen)
                {
                    var panel = new StackPanel { Width = 100 };
                    var style = Application.Current.TryFindResource("MaterialDesignCircularProgressBar") as Style;
                    var progress = new ProgressBar
                    {
                        Width = 40,
                        Height = 40,
                        Margin = new Thickness(20),
                        IsIndeterminate = true,
                        Style = style,
                    };
                    panel.Children.Add(progress);
                    DialogHostControl.DialogContent = panel;
                }
            });
        }

        private void Header_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Restore_Click(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton toggle)
            {
                WindowState = toggle.IsChecked == true ? WindowState.Maximized : WindowState.Normal;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
