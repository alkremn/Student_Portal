﻿using Student_Portal.Models;
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

            var type = (AssessmentType)value;
                switch (type)
                {
                    case AssessmentType.Objective:
                        return "Objective Assessment";
                    case AssessmentType.Performance:
                        return "Performance Assessment";
                }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var type = (string)value;
            switch (type)
            {
                case "Objective Assessment":
                    return AssessmentType.Objective;
                case "Performance Assessment":
                    return AssessmentType.Performance;
            }
            return null;
        }
    }
}
