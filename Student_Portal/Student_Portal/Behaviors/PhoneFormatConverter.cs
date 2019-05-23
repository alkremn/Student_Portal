using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
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
