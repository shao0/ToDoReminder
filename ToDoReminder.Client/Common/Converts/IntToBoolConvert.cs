namespace ToDoReminder.Client.Common.Converts
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    public class IntToBoolConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is int i)
            {
                return i > 0;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
            {
                return b ? 1 : 0;
            }
            return 0;
        }
    }
}
