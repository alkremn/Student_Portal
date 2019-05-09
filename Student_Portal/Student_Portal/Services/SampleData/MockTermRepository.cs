using System;
using System.Collections.Generic;

namespace Student_Portal.Models
{
    public static class MockTermRepository
    {
       
        public static IEnumerable<Term> GetTermList()
        {
            return new List<Term>
            {
                new Term { Id = 1, Title = "Term 1", StartDate = DateTime.Now, EndDate = DateTime.Now },
                new Term { Id = 2, Title = "Term 2", StartDate = DateTime.Now, EndDate = DateTime.Now },
                new Term { Id = 3, Title = "Term 3", StartDate = DateTime.Now, EndDate = DateTime.Now },
                new Term { Id = 4, Title = "Summer Term", StartDate = DateTime.Now, EndDate = DateTime.Now }
            };
        }

    }
}
