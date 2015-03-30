using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DrugHouse.ViewModel.ValueConverter
{
    public class VisibilityConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var bValue = (bool)value;
            return bValue ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visibility = (Visibility)value;

            return visibility == Visibility.Visible;
        }
    }
}