using System;
using System.Collections.Generic;

namespace Student_Portal.Model
{
    public class MockTermRepository
    {
        public Term GetSingleTerm()
        {
            return new Term { TermId = 1, Title = "Summer Term", StartDate = DateTime.Now, EndDate = DateTime.Now };
        }

        public IEnumerable<Term> GetTermList()
        {
            return new List<Term>
            {
                new Term { TermId = 1, Title = "Term 1", StartDate = DateTime.Now, EndDate = DateTime.Now },
                new Term { TermId = 2, Title = "Term 2", StartDate = DateTime.Now, EndDate = DateTime.Now },
                new Term { TermId = 3, Title = "Term 3", StartDate = DateTime.Now, EndDate = DateTime.Now },
                new Term { TermId = 4, Title = "Summer Term", StartDate = DateTime.Now, EndDate = DateTime.Now }
            };
        }

    }
}
