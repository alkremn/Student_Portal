using Student_Portal.Models;
using Student_Portal.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Plugin.LocalNotifications;
using System.Windows.Input;
using Xamarin.Forms;

namespace Student_Portal.ViewModels
{
    public class AddNewAssessmentViewModel : BaseViewModel
    {
        private Assessment _assessment;
        private int _courseId;
        private AssessmentDataService _assessmentDS;
        private const string NEW_ASSESSMENT = "New Assessment";
        private const string MODIFY_ASSESSMENT = "Modify Assessment";
        private const string OBJECTIVE = "Objective";
        private const string PERFORMANCE = "Performance";
        private const string SAVE = "Save";
        private const string UPDATE = "Update";

        public ObservableCollection<string> AvailableTypes { get; set; }
        public string Title { get; set; }
        public string ButtonTitle { get; set; }
        public string NameAssessment { get; set; }

        public Command SaveCommand { get; }
        public ICommand CancelCommand { get; }

        private string _assessmentTypeSelected;
        public string AssessmentTypeSelected
        {
            get => _assessmentTypeSelected;
            set
            {
                if(value != null)
                {
                    _assessmentTypeSelected = value;
                    OnPropertyChanged();
                    SaveCommand.ChangeCanExecute();
                }
            }
        }

        private DateTime _startDate;
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
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
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        public AddNewAssessmentViewModel(AssessmentDataService assessmentDS, Assessment assessment, List<Assessment> assessmentList, int courseId)
        {
            _assessmentDS = assessmentDS;
            _assessment = assessment;
            _courseId = courseId;
            AvailableTypes = new ObservableCollection<string>();

            if (assessment != null)
            {
                InitData(assessment);
                ButtonTitle = UPDATE;
            }
            else
            {
                Title = NEW_ASSESSMENT;
                ButtonTitle = SAVE;
                _startDate = DateTime.Today;
                _endDate = DateTime.Today;
            }
            InitAvailableTypeList(assessmentList);
            SaveCommand = new Command(OnSaveClicked, CanOnSaveClicked);
            CancelCommand = new Command(OnCancelClicked);
        }

        private void InitAvailableTypeList(List<Assessment> assessments)
        {
            if (assessments.Count == 1)
            {
                if (assessments[0].Type == OBJECTIVE)
                    AvailableTypes.Add(PERFORMANCE);
                else
                    AvailableTypes.Add(OBJECTIVE);
            }
            else if(assessments.Count == 0)
            {
                AvailableTypes.Add(PERFORMANCE);
                AvailableTypes.Add(OBJECTIVE);
            }
        }

        private void InitData(Assessment assessment)
        {
            Title = MODIFY_ASSESSMENT;
            NameAssessment = assessment.Name;
            _assessmentTypeSelected = assessment.Type;
            _startDate = assessment.StartDate;
            _endDate = assessment.EndDate;
        }

        private async void OnSaveClicked(object obj)
        {
            if (_assessment == null)
            {
                _assessment = new Assessment();
            }
            _assessment.Name = NameAssessment;
            _assessment.Type = AssessmentTypeSelected;
            _assessment.StartDate = _startDate;
            _assessment.EndDate = _endDate;
            _assessment.CourseId = _courseId;

            //Creates Notifications on Start and End Date
            CrossLocalNotifications.Current.Show($"{_assessment.Type} {_assessment.Name}", "Assessment start", 1, StartDate);
            CrossLocalNotifications.Current.Show($"{_assessment.Type} {_assessment.Name}", "Assessment end", 2, EndDate);

            //Saves assessment
            await _assessmentDS.SaveAssessmentAsync(_assessment);
            MessagingCenter.Send(this, SAVE);
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private bool CanOnSaveClicked(object arg)
        {
            return !string.IsNullOrWhiteSpace(NameAssessment);
        }

        private async void OnCancelClicked(object obj)
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
