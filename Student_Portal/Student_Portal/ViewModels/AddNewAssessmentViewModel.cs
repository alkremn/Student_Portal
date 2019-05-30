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

        public ObservableCollection<string> AvailableTypes { get; }
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

        private DateTime _startDateSelected;
        public DateTime StartDateSelected
        {
            get => _startDateSelected;
            set
            {
                _startDateSelected = value;
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();

                if (value.Date <= EndDateSelected.Date)
                {
                    IsStartEndDateValid = true;
                }
                else
                {
                    IsStartEndDateValid = false;
                }

            }
        }

        private DateTime _endDateSelected;
        public DateTime EndDateSelected
        {
            get => _endDateSelected;
            set
            {
                _endDateSelected = value;
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();

                if (value.Date >= StartDateSelected.Date)
                {
                    IsStartEndDateValid = true;
                }
                else
                {
                    IsStartEndDateValid = false;
                }
            }
        }

        private bool isStartEndDateValid;
        public bool IsStartEndDateValid
        {
            get => isStartEndDateValid;
            set
            {
                isStartEndDateValid = value;
                OnPropertyChanged();
            }
        }

        //Constructor
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
                _startDateSelected = DateTime.Today;
                _endDateSelected = DateTime.Today;
            }
            IsStartEndDateValid = true;
            InitAvailableTypeList(assessment, assessmentList);
            SaveCommand = new Command(OnSaveClicked, CanOnSaveClicked);
            CancelCommand = new Command(OnCancelClicked);
        }

        //Populates list of Available types
        private void InitAvailableTypeList(Assessment assessment, List<Assessment> assessments)
        {
            switch (assessments.Count)
            {
                case 2:
                    AvailableTypes.Add(assessment.Type);
                    break;
                case 1:
                    if(assessment != null)
                    {
                        AvailableTypes.Add(PERFORMANCE);
                        AvailableTypes.Add(OBJECTIVE);
                    }
                    else
                    {
                        if (assessments[0].Type == OBJECTIVE)
                            AvailableTypes.Add(PERFORMANCE);
                        else
                            AvailableTypes.Add(OBJECTIVE);
                    }
                    break;
                case 0:
                    AvailableTypes.Add(PERFORMANCE);
                    AvailableTypes.Add(OBJECTIVE);
                    break;
            }
        }
        
        //Initializes data if assessment is not null
        private void InitData(Assessment assessment)
        {
            Title = MODIFY_ASSESSMENT;
            NameAssessment = assessment.Name;
            _assessmentTypeSelected = assessment.Type;
            _startDateSelected = assessment.StartDate;
            _endDateSelected = assessment.EndDate;
        }

        //On Save Method
        private async void OnSaveClicked(object obj)
        {
            if (_assessment == null)
            {
                _assessment = new Assessment();
            }
            _assessment.Name = NameAssessment;
            _assessment.Type = AssessmentTypeSelected;
            _assessment.StartDate = _startDateSelected;
            _assessment.EndDate = _endDateSelected;
            _assessment.CourseId = _courseId;

            //Creates Notifications on Start and End Date
            CrossLocalNotifications.Current.Show($"{_assessment.Type} {_assessment.Name}", "Assessment start", 1, StartDateSelected);
            CrossLocalNotifications.Current.Show($"{_assessment.Type} {_assessment.Name}", "Assessment end", 2, EndDateSelected);

            //Saves assessment
            await _assessmentDS.SaveAssessmentAsync(_assessment);
            MessagingCenter.Send(this, SAVE);
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        //Validates information to save
        private bool CanOnSaveClicked(object arg)
        {
            bool StartEndValid = _startDateSelected <= _endDateSelected;
            return !string.IsNullOrWhiteSpace(NameAssessment) && StartEndValid;
        }

        //Returns to previous page
        private async void OnCancelClicked(object obj)
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
