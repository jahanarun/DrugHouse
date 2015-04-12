using System;
using System.Globalization;
using System.Windows.Data;
using DrugHouse.Shared.Enumerations;

namespace DrugHouse.ViewModel.ValueConverter
{
    public class DrugTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DrugType result;
            Enum.TryParse((string) value, true, out result);
            return result;
        }
    }
}