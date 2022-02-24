using MaterialDesignThemes.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ToDoReminder.Client.Common.Controls
{
    /// <summary>
    /// TimeSliceControl.xaml 的交互逻辑
    /// </summary>
    public partial class TimeSliceControl : UserControl
    {
        public TimeSliceControl()
        {
            InitializeComponent();
        }

        public DateTime? StartDateTime
        {
            get { return (DateTime?)GetValue(StartDateTimeProperty); }
            set { SetValue(StartDateTimeProperty, value); }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public static readonly DependencyProperty StartDateTimeProperty =
            DependencyProperty.Register("StartDateTime", typeof(DateTime?), typeof(TimeSliceControl), new PropertyMetadata(null));


        public DateTime? EndDateTime
        {
            get { return (DateTime?)GetValue(EndDateTimeProperty); }
            set { SetValue(EndDateTimeProperty, value); }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public static readonly DependencyProperty EndDateTimeProperty =
            DependencyProperty.Register("EndDateTime", typeof(DateTime?), typeof(TimeSliceControl), new PropertyMetadata(null));




        public void StartOpenedEventHandler(object sender, DialogOpenedEventArgs eventArgs)
        {
            StartDate.SelectedDate = StartTime.Time = StartDateTime ?? DateTime.Now;
        }

        public void StartClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (Convert.ToBoolean(Convert.ToUInt16(eventArgs.Parameter)) && StartDate.SelectedDate is DateTime selectedDate)
            {
                DateTime combined = selectedDate.Date.AddSeconds(StartTime.Time.TimeOfDay.TotalSeconds);
                StartDateTime = combined;
            }
        }
        public void EndOpenedEventHandler(object sender, DialogOpenedEventArgs eventArgs)
        {
            EndDate.SelectedDate = EndTime.Time = EndDateTime ?? DateTime.Now;
        }

        public void EndClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (Convert.ToBoolean(Convert.ToUInt16(eventArgs.Parameter)) && EndDate.SelectedDate is DateTime selectedDate)
            {
                DateTime combined = selectedDate.Date.AddSeconds(EndTime.Time.TimeOfDay.TotalSeconds);
                EndDateTime = combined;
            }
        }
    }
}
