using Student_Portal.Models;
using Student_Portal.Services;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Essentials;
using Student_Portal.Views;
using System.Linq;

namespace Student_Portal.ViewModels
{
    public class CourseDetailPageViewModel : BaseViewModel
    {
        private Course _selectedCourse;
        private AssessmentDataService _assessmentDS;
        private const string COUNT_LESS_TWO = "count<2";
        private const string COUNT_EQ_TWO = "count=2";
        private const string SAVE = "Save";

        public string Title { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public string Status { get; }
        public string InsName { get; }
        public string Phone { get; }
        public string Email { get; }
        public string Notes { get; }

        public ObservableCollection<Assessment> Assessments { get; private set; }

        public Command ShareNotesCommand { get; }
        public Command AddAssessmentCommand { get; }
        public Command ModifyCommand { get; }
        public Command DeleteCommand { get; }
        public Command BackCommand { get; }


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
            GetAllAssessments(selectedCourse.Id);
            ShareNotesCommand = new Command(OnShareNotesClicked);
            AddAssessmentCommand = new Command(OnAddAssessmentClicked, CanAddAssessmentClicked);
            ModifyCommand = new Command(OnModifyClicked);
            DeleteCommand = new Command(OnDeleteClicked);
            BackCommand = new Command(OnBackClicked);

            MessagingCenter.Subscribe<AddNewAssessmentViewModel>(this, SAVE, (obj) => OnAssessmentCreate());
        }

        private void OnAssessmentCreate()
        {
            GetAllAssessments(_selectedCourse.Id);
            CheckAssessmentListCount(Assessments);
        }

        public async void GetAllAssessments(int courseId)
        {
            var assessmentsById = await _assessmentDS.GetAllAssessmentsByCourseIdAsync(courseId);
            Assessments.Clear();
            assessmentsById.ForEach(a => Assessments.Add(a));
            CheckAssessmentListCount(Assessments);
        }

        private void OnShareNotesClicked(object obj)
        {
            Share.RequestAsync(new ShareTextRequest
            {
                Text = Notes,
                Title = "Notes"
            });
        }

        private async void OnAddAssessmentClicked(object obj)
        {
            await App.Current.MainPage.Navigation.PushAsync(
                new AddNewAssessmentPage(_assessmentDS, null, Assessments.ToList(), _selectedCourse.Id));
        }

        private bool CanAddAssessmentClicked(object arg)
        {
            return Assessments.Count < 2;
        }

        private async void OnModifyClicked(object obj)
        {
            if (obj == null)
                return;
            Assessment assessment = obj as Assessment;
            await App.Current.MainPage.Navigation.PushAsync(
                new AddNewAssessmentPage(_assessmentDS, assessment, Assessments.ToList(), _selectedCourse.Id));
        }

        private async void OnDeleteClicked(object obj)
        {
            if (obj == null)
                return;
            Assessment assessment = obj as Assessment;
            await _assessmentDS.DeleteAssessmentAsync(assessment);
            Assessments.Remove(assessment);
            CheckAssessmentListCount(Assessments);
        }

        private async void OnBackClicked(object obj)
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private void CheckAssessmentListCount(ObservableCollection<Assessment> list)
        {
            if (list.Count < 2)
            {
                MessagingCenter.Send(this, COUNT_LESS_TWO);
            }
            else
            {
                MessagingCenter.Send(this, COUNT_EQ_TWO);
            }
        }
    }
}
