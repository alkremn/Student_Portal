using SQLite;
using System;
using System.Collections.Generic;

namespace Student_Portal.Model
{
    [Table("Terms")]
    public class Term
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public List<Course> Courses { get; private set; } = new List<Course>();
    }
}
