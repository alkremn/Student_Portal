using Student_Portal.Models;
using Student_Portal.Services;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Student_Portal.ViewModels
{
    public class CourseDetailPageViewModel : BaseViewModel
    {
        private Course _selectedCourse;
        private AssessmentDataService _assessmentDS;
        public string Title { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public string Status { get; }
        public string InsName { get; }
        public string Phone { get; }
        public string Email { get; }
        public string Notes { get; }

        public ObservableCollection<Assessment> Assessments { get; private set; }

        public Command BackCommand { get; private set; }

        public CourseDetailPageViewModel(Course selectedCourse, AssessmentDataService assessmentDS)
        {
            _selectedCourse = selectedCourse;
            _assessmentDS = assessmentDS;

            Title = selectedCourse.Title;
            StartDate = selectedCourse.StartDate;
            EndDate = selectedCourse.EndDate;
            Status = selectedCourse.Status;
            InsName = selectedCourse.InstructorName;
            Phone = selectedCourse.InstructorPhone;
            Email = selectedCourse.InstructorEmail;
            Notes = selectedCourse.Notes;

            Assessments = new ObservableCollection<Assessment>();
            GetAllAssessments(assessmentDS, selectedCourse.Id);
            BackCommand = new Command(OnBackClicked);
        }

        public async void GetAllAssessments(AssessmentDataService assessmentDS, int courseId)
        {
            var assessmentsById = await assessmentDS.GetAllAssessmentsByCourseIdAsync(courseId);
            Assessments.Clear();
            assessmentsById.ForEach(a => Assessments.Add(a));
        }

        private async void OnBackClicked(object obj)
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
