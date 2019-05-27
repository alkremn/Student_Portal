using Student_Portal.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Student_Portal.Models
{
    public static class SampleDataRepository
    {
        public static async Task SetMockData(TermDataService _termData, CourseDataService _courseData, AssessmentDataService _assessmentData)
        {
            var terms = await _termData.GetTermsAsync();
            if (terms.Count == 0)
            {
                string title = "Summer Term";

                await _termData.SaveTermAsync(new Term()
                {
                    Title = title,
                    StartDate = DateTime.Today + TimeSpan.FromDays(7),
                    EndDate = DateTime.Today + TimeSpan.FromDays(30),
                });
                var term = await _termData.GetTermByTitleAsync(title);

                await _courseData.SaveCourseAsync(
                    new Course()
                    {
                        Title = "C++",
                        StartDate = DateTime.Today + TimeSpan.FromDays(7),
                        EndDate = DateTime.Today + TimeSpan.FromDays(30),
                        Status = "Plan To Take",
                        InstructorName = "Alexey Kremnev",
                        InstructorPhone = "3852218919",
                        InstructorEmail = "alkremn@gmail.com",
                        IsExisting = true,
                        Notes = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                        TermId = term.Id
                    });
                var courses = await _courseData.GetAllCoursesByTermIdAsync(term.Id);

                await _assessmentData.SaveAssessmentListAsync(
                    new List<Assessment>()
                        {
                        new Assessment()
                        {
                            Name = "C++",
                            StartDate = DateTime.Today + TimeSpan.FromDays(15),
                            EndDate = DateTime.Today + TimeSpan.FromDays(45),
                            Type = "Objective",
                            CourseId = courses[0].Id
                        },
                        new Assessment()
                        {
                                Name = "Math",
                            StartDate = DateTime.Today + TimeSpan.FromDays(45),
                            EndDate = DateTime.Today + TimeSpan.FromDays(60),
                            Type = "Performance",
                            CourseId = courses[0].Id
                        }
                    });
            }
        }
    }
}
