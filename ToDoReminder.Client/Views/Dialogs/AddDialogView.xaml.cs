using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToDoReminder.Client.Common.Models;
using ToDoReminder.Client.ViewModels.Dialogs;

namespace ToDoReminder.Client.Views.Dialogs
{
    /// <summary>
    /// AddDialogView.xaml 的交互逻辑
    /// </summary>
    public partial class AddDialogView : UserControl
    {
        public AddDialogView()
        {
            InitializeComponent();
        }

        public void CombinedDialogOpenedEventHandler(object sender, DialogOpenedEventArgs eventArgs)
        {
            if (DataContext is AddDialogViewModel viewModel && viewModel.Model is ToDoReminderModel model)
            {
                CombinedCalendar.SelectedDate = model.ReminderDateTime;
                CombinedClock.Time = model.ReminderDateTime;
            }
        }

        public void CombinedDialogClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (Convert.ToBoolean(Convert.ToUInt16(eventArgs.Parameter)) && CombinedCalendar.SelectedDate is DateTime selectedDate && DataContext is AddDialogViewModel viewModel && viewModel.Model is ToDoReminderModel model)
            {
                DateTime combined = selectedDate.Date.AddSeconds(CombinedClock.Time.TimeOfDay.TotalSeconds);
                model.ReminderDateTime = combined;
            }
        }
    }
}
