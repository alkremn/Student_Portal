using SQLite;
using Student_Portal.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Student_Portal.Services
{
    public class CourseDataService
    {
        private readonly SQLiteAsyncConnection database;

        public CourseDataService(SQLiteAsyncConnection database)
        {
            this.database = database;
            database.CreateTableAsync<Course>().Wait();
        }
        public Task<List<Course>> GetAllCoursesByTermIdAsync(int termId)
        {
            return database.Table<Course>().Where(c => c.TermId == termId).ToListAsync();
        }
        public Task<int> SaveCourseAsync(Course course)
        {
            if (course.Id == 0)
            {
                return database.InsertAsync(course);
            }
            else
            {
                return database.UpdateAsync(course);
            }
        }
        public Task<Course> GetCourseByIdAsync(int courseId)
        {
            return database.Table<Course>().Where(c => c.Id == courseId).FirstOrDefaultAsync();
        }
        public Task<int> DeleteCourseAsync(Course course)
        {
            return database.DeleteAsync(course);
        }
    }
}
