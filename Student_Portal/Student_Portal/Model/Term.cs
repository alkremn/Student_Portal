using SQLite;
using System;
using System.Collections.ObjectModel;

namespace Student_Portal.Model
{
    [Table("Term")]
    public class Term
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public ObservableCollection<Course> Courses { get; private set; } = new ObservableCollection<Course>();
    }
}
