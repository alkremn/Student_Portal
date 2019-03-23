using System;
using System.Collections.ObjectModel;

namespace Student_Portal.Models
{
    public class Course
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public CourseStatus Status { get; set; }
        public CourseInstructor Insructor { get; set; }
        public string Notes { get; set; }
        public ObservableCollection<AssestmentType> Assestments { get; private set; } = new ObservableCollection<AssestmentType>();

    }
}
