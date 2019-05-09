using Student_Portal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Student_Portal.Services.SampleData
{
    public static class MockAssessmentRepository
    {
        public static IEnumerable<Assessment> GetSampleAssessmentList()
        {
            return new List<Assessment>
            {
                new Assessment { Id = 1, Name = "Performance assessment", Type = AssessmentType.Performance, CourseId = 1},
                new Assessment { Id = 2, Name = "Objective assessment", Type = AssessmentType.Objective, CourseId = 1},
            };
        }
    }
}
