using SQLite;
using Student_Portal.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Student_Portal.Services
{
    public class AssessmentDataService
    {
        private readonly SQLiteAsyncConnection database;

        public AssessmentDataService(SQLiteAsyncConnection database)
        {
            this.database = database;
            database.CreateTableAsync<Assessment>().Wait();
        }

        public Task<List<Assessment>> GetAllAssessmentsByCourseIdAsync(int courseId)
        {
            return database.Table<Assessment>().Where(a => a.CourseId == courseId).ToListAsync();
        }

        public Task<int> SaveAssessmentAsync(Assessment assessment)
        {
            if (assessment.Id == 0)
            {
                return database.InsertAsync(assessment);
            }
            else
            {
                return database.UpdateAsync(assessment);
            }
        }

        public Task<Assessment> GetAssessmentByIdAsync(int assessmentId)
        {
            return database.Table<Assessment>().Where(a => a.Id == assessmentId).FirstOrDefaultAsync();
        }

        public Task<int> DeleteAssessmentAsync(Assessment assessment)
        {
            return database.DeleteAsync(assessment);
        }

        public async Task DeleteAssessmentsByCourseIdAsync(int courseId)
        {
            var assessmentList = await database.Table<Assessment>().Where(a => a.CourseId == courseId).ToListAsync();

            foreach (Assessment assessment in assessmentList)
            {
                await database.DeleteAsync(assessment);
            }
        }
    }
}
