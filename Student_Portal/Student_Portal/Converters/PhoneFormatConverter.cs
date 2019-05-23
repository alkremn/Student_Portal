using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace Student_Portal.Converters
{
    public class PhoneFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            if(long.TryParse((string)value, out long result))
            {
                if (result == 0)
                    return "";

                return string.Format("{0:(###)###-####}",result);
            }
            return "";
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace((string)value))
                return 0;

            string stringValue = (string)value;
            string formatedString = Regex.Replace(stringValue, @"[()-]", "");


            if (long.TryParse((string)value, out long result))
            {

            }
            return 0;
        }
    }
}
