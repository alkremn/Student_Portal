using System;
using System.Collections.Generic;

namespace Student_Portal.Models
{
    public static class MockCourseRepository
    {
        public static IEnumerable<Course> GetCourseList()
        {
            return new List<Course>
            {
                new Course{ Id = 1, TermId = 1, Title = "Programming", StartDate = DateTime.Now, EndDate = DateTime.Now},
                new Course{ Id = 2, TermId = 1, Title = "Math", StartDate = DateTime.Now, EndDate = DateTime.Now},
                new Course{ Id = 3, TermId = 1, Title = "English 1", StartDate = DateTime.Now, EndDate = DateTime.Now},
                new Course{ Id = 4, TermId = 1, Title = "SQL", StartDate = DateTime.Now, EndDate = DateTime.Now},
            };
        }

    }
}
