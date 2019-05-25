using Student_Portal.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Student_Portal.Converters
{
    public class AssessmentTypeListToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";

            ObservableCollection<string> assessmentTypes = new ObservableCollection<string>();

            var types = value as ObservableCollection<AssessmentType>;
            foreach(var type in types)
            {
                switch (type)
                {
                    case AssessmentType.Objective:
                        assessmentTypes.Add("Objective");
                        break;
                    case AssessmentType.Performance:
                        assessmentTypes.Add("Performance");
                        break;
                    default:
                        return null;
                }
            }
            return assessmentTypes;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;


            //TODO  finish implementation

            string typeString = value as string;
            switch (typeString)
            {
                case "Objective":
                    return AssessmentType.Objective;
                case "Performance":
                    return AssessmentType.Performance;
                default:
                    return null;
            }
        }
    }
}
