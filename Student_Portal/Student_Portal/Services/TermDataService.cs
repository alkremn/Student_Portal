using SQLite;
using Student_Portal.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Student_Portal.Services
{
    public class TermDataService
    {
        readonly SQLiteAsyncConnection database;

        public TermDataService(SQLiteAsyncConnection database)
        {
            this.database = database;
            database.CreateTableAsync<Term>();
        }
        public Task<List<Term>> GetTermsAsync()
        {
            return database.Table<Term>().ToListAsync();
        }
        public Task<Term> GetTermAsync(int id)
        {
            return database.Table<Term>().Where(t => t.Id == id).FirstOrDefaultAsync();
        }
        public Task<int> SaveTermAsync(Term term)
        {
            if (term.Id == 0)
                return database.InsertAsync(term);
            else
                return database.UpdateAsync(term);
        }
        public Task<int> DeleteTermAsync(Term term)
        {
            return database.DeleteAsync(term);
        }

    }
}
