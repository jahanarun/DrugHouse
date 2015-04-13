using System;
using System.Globalization;
using System.Windows.Data;

namespace DrugHouse.ViewModel.ValueConverter
{
    public class SelecteTabConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return System.Windows.Visibility.Collapsed;

            var type = value.GetType();
           
            if (type.Name == (string) parameter)            
                return System.Windows.Visibility.Visible;

            return System.Windows.Visibility.Collapsed;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}