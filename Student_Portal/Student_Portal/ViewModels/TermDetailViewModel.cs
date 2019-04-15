using Student_Portal.Models;
using Student_Portal.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Student_Portal.ViewModels
{
    public class TermDetailViewModel : BaseViewModel
    {
        private CourseDataService _courseData;
        public string Title { get; set; }

        public TermDetailViewModel(CourseDataService courseData, Term term)
        {
            _courseData = courseData;
            Title = term.Title;
        }

    }
}
