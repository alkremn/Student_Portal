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
        private const string CREATED = "created";
        private bool isStartDateSelected = false;
        private bool isEndDateSelected = false;

        public string Title { get; set; }
        public ObservableCollection<AssessmentType> AvailableTypes { get; }

        public Command SaveCommand { get; }
        public ICommand CancelCommand { get; }

        private string _assessmentType;
        public string AssessmentTypeSelected
        {
            get => _assessmentType;
            set
            {
                _assessmentType = value;
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                isStartDateSelected = true;
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();

            }
        }

        private DateTime _endDate;
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                isEndDateSelected = true;
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
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
            {
                InitData(assessment);
            }
            else
            {
                Title = NEW_ASSESSMENT;
            }

            InitAssessmentTypeList(assessmentList);
            SaveCommand = new Command(OnSaveClicked, CanOnSaveClicked);
            CancelCommand = new Command(OnCancelClicked);
        }

        private void InitData(Assessment assessment)
        {
            Title = MODIFY_ASSESSMENT;
            AssessmentTypeSelected = assessment.Type == AssessmentType.Objective ? "Objective Assessment" : "Performance Assessment";
            StartDate = assessment.StartDate;
            EndDate = assessment.EndDate;
        }

        private void InitAssessmentTypeList(List<Assessment> assessmentList)
        {

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

        private async void OnSaveClicked(object obj)
        {
            var assessment = new Assessment()
            {
                Type = AssessmentTypeSelected == "Objective Assessment" ? AssessmentType.Objective : AssessmentType.Performance,
                StartDate = _startDate,
                EndDate = _endDate,
                CourseId = courseId
            };
            MessagingCenter.Send(assessment, CREATED);
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private bool CanOnSaveClicked(object arg)
        {
            return !string.IsNullOrWhiteSpace(AssessmentTypeSelected) && isStartDateSelected && isEndDateSelected;
        }

        private async void OnCancelClicked(object obj)
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
