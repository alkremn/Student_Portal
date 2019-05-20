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
                new Assessment { Id = 1, Name = "Performance assessment", StartDate = DateTime.Now - TimeSpan.FromDays(4), EndDate = DateTime.Now + TimeSpan.FromDays(6) , CourseId = 1},
                new Assessment { Id = 2, Name = "Objective assessment",StartDate = DateTime.Now - TimeSpan.FromDays(10), EndDate = DateTime.Now + TimeSpan.FromDays(10), CourseId = 1},
            };
        }
    }
}
