using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Store.Ui.Converter
{
    class BytesToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var imageBytes = value as byte[];
            if (imageBytes != null)
            {
                return ImageSource.FromStream(() => { return new MemoryStream(imageBytes); });

            }else
            {
                return null;
            }
            

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
