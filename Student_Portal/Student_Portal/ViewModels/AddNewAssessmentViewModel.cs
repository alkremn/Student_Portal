using Student_Portal.Models;
using Student_Portal.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Student_Portal.ViewModels
{
    public class AddNewAssessmentViewModel : BaseViewModel
    {
        private Assessment assessment;
        private int courseId;
        private AssessmentDataService assessmentDS;
        private const string NEW_ASSESSMENT = "New Assessment";
        private const string MODIFY_ASSESSMENT = "Modify Assessment";
        private DateTime startDate;
        private DateTime endDate;

        public string Title { get; set; }
        public string AssessmentName { get; set; }
        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public DateTime StartDate
        {
            get => startDate;
            set
            {
                startDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime EndDate
        {
            get => endDate;
            set
            {
                endDate = value;
                OnPropertyChanged();
            }
        }

        public AddNewAssessmentViewModel(AssessmentDataService assessmentDS, Assessment assessment, int courseId)
        {
            this.assessmentDS = assessmentDS;
            this.assessment = assessment;
            this.courseId = courseId; 
            if (assessment != null)
                InitData(assessment);
            else
                Title = NEW_ASSESSMENT;
            SaveCommand = new Command(async ()=> await OnSaveClicked());
            CancelCommand = new Command(OnCancelClicked);
        }
       
        private void InitData(Assessment assessment)
        {
            Title = MODIFY_ASSESSMENT;
            AssessmentName = assessment.Name;
            StartDate = assessment.StartDate;
            EndDate = assessment.EndDate;
        }

        private async Task OnSaveClicked()
        {
            var assessment = new Assessment()
            {
                Name = AssessmentName,
                StartDate = startDate,
                EndDate = endDate,
                CourseId = courseId
            };
            await assessmentDS.SaveAssessmentAsync(assessment);
            await App.Current.MainPage.Navigation.PopModalAsync();
        }

        private async void OnCancelClicked(object obj)
        {
            await App.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}
