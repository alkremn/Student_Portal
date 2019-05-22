using Student_Portal.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Student_Portal.Converters
{
    public class AssessmentTypeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            AssessmentType type = (AssessmentType)value;
            switch (type)
            {
                case AssessmentType.Objective:
                    return "Objective Assessment";
                case AssessmentType.Performance:
                    return "Performance Assessment";
                default:
                    return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            string typeString = value as string;
            switch (typeString)
            {
                case "Objective Assessment":
                    return AssessmentType.Objective;
                case "Performance Assessment":
                    return AssessmentType.Performance;
                default:
                    return null;
            }
        }
    }
}
