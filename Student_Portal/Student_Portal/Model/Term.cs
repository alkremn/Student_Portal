using System;
using System.Collections.ObjectModel;

namespace Student_Portal.Model
{
    public class Term
    {
        public string Title { get; private set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ObservableCollection<Course> Courses { get; private set; } = new ObservableCollection<Course>();
    }
}
