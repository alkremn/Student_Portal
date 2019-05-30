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
        private const string SAVE = "Save";

        public string Title { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public string Status { get; }
        public string InsName { get; }
        public string Phone { get; }
        public string Email { get; }
        public string Notes { get; }

        public ObservableCollection<Assessment> Assessments { get; }
        public Command ShareNotesCommand { get; }
        public Command AddAssessmentCommand { get; }
        public Command ModifyCommand { get; }
        public Command DeleteCommand { get; }
        public Command BackCommand { get; }

        private bool _isVisible;
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                _isVisible = value;
                OnPropertyChanged();
            }
        }

        //constructor
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
            AddAssessmentCommand = new Command(OnAddAssessmentClicked);
            ModifyCommand = new Command(OnModifyClicked);
            DeleteCommand = new Command(OnDeleteClicked);
            BackCommand = new Command(OnBackClicked);

            MessagingCenter.Subscribe<AddNewAssessmentViewModel>(this, SAVE, (obj) => GetAllAssessments(_selectedCourse.Id));
        }

        //Returns all assessments by the given course id
        public async void GetAllAssessments(int courseId)
        {
            var assessmentsById = await _assessmentDS.GetAllAssessmentsByCourseIdAsync(courseId);
            Assessments.Clear();
            assessmentsById.ForEach(a => Assessments.Add(a));
            CheckAssessmentListCount(Assessments);
        }

        //shares notes
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

        private async void OnModifyClicked(object obj)
        {
            if (obj == null)
                return;
            Assessment assessment = obj as Assessment;
            await App.Current.MainPage.Navigation.PushAsync(
                new AddNewAssessmentPage(_assessmentDS, assessment, Assessments.ToList(), _selectedCourse.Id));
        }

        //Method that deletes assessment
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

        //sets add button visibility if list count.
        private void CheckAssessmentListCount(ObservableCollection<Assessment> list)
        {
            IsVisible = list.Count < 2 ? true : false;
        }
    }
}
