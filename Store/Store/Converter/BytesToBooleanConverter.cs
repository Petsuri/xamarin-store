using System;
using System.Globalization;
using Xamarin.Forms;

namespace Store.Ui.Converter
{
    internal class BytesToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var bytes = (value as byte[]);
            if (bytes != null)
            {
                return true;
            }else
            {
                return false;
            }

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
