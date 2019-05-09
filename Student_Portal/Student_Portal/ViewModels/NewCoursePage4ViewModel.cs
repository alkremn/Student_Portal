using Student_Portal.Models;
using System;
using Student_Portal.Services.SampleData;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;

namespace Student_Portal.ViewModels
{
    public class NewCoursePage4ViewModel
    {
        public ObservableCollection<Assessment> Assessments { get; }

        public NewCoursePage4ViewModel()
        {
            Assessments = new ObservableCollection<Assessment>();
            LoadData();
        }
        private void LoadData()
        {
            var assessments = MockAssessmentRepository.GetSampleAssessmentList();
            assessments.ToList().ForEach(a => Assessments.Add(a));
        }
    }
}
