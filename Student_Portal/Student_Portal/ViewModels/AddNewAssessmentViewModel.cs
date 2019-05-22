using Student_Portal.Models;
using Student_Portal.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Student_Portal.ViewModels
{
    public class AddNewAssessmentViewModel : BaseViewModel
    {
        private Assessment assessment;
        private List<Assessment> assessmentList;
        private int courseId;
        private AssessmentDataService assessmentDS;
        private const string NEW_ASSESSMENT = "New Assessment";
        private const string MODIFY_ASSESSMENT = "Modify Assessment";
        private DateTime startDate;
        private DateTime endDate;

        public string Title { get; set; }
        public string AssessmentName { get; set; }
        public ObservableCollection<AssessmentType> AvailableTypes { get; }
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

        public AddNewAssessmentViewModel(AssessmentDataService assessmentDS, Assessment assessment, List<Assessment> assessmentList, int courseId)
        {
            this.assessmentDS = assessmentDS;
            this.assessment = assessment;
            this.assessmentList = assessmentList;
            this.courseId = courseId;
            AvailableTypes = new ObservableCollection<AssessmentType>();
            if (assessment != null)
                InitData(assessment, assessmentList);
            else
                Title = NEW_ASSESSMENT;
            SaveCommand = new Command(async ()=> await OnSaveClicked());
            CancelCommand = new Command(OnCancelClicked);
        }
       
        private void InitData(Assessment assessment, List<Assessment> assessmentList)
        {
            Title = MODIFY_ASSESSMENT;
            AssessmentName = assessment.Name;
            StartDate = assessment.StartDate;
            EndDate = assessment.EndDate;

            switch (assessmentList.Count)
            {
                case 0:
                    AvailableTypes.Add(AssessmentType.Objective);
                    AvailableTypes.Add(AssessmentType.Performance);
                    break;
                case 1:
                    if (assessmentList[0].Type == AssessmentType.Objective)
                        AvailableTypes.Add(AssessmentType.Performance);
                    else
                        AvailableTypes.Add(AssessmentType.Objective);
                    break;
            }
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
            MessagingCenter.Send(assessment, "Created");
            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

        private async void OnCancelClicked(object obj)
        {
            await App.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}
