using Student_Portal.Views;
using System;
using Xamarin.Forms;
using Student_Portal.Models;

namespace Student_Portal.ViewModels
{
    public class NewCoursePage1ViewModel:BaseViewModel
    {
        private bool IsStatusSelected = false;
        private Course _course;
        private Term _term;

        public Command CancelCommand { get; }
        public Command NextCommand { get; }

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
                NextCommand.ChangeCanExecute();
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

        private DateTime _startDateSelected;
        public DateTime StartDateSelected
        {
            get => _startDateSelected;
            set
            {
                _startDateSelected = value;
                OnPropertyChanged();
                NextCommand.ChangeCanExecute();

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
                NextCommand.ChangeCanExecute();

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

        private string _selectedStatus;
        public string SelectedStatus
        {
            get => _selectedStatus;
            set
            {
                _selectedStatus = value;
                IsStatusSelected = true;
                OnPropertyChanged();
                NextCommand.ChangeCanExecute();
            }
        }

        public NewCoursePage1ViewModel(Course selectedCourse, Term term)
        {
            _course = selectedCourse;
            _term = term;

            if (_course.IsExisting)
            {
                InitCourseData(_course);
            }
            else
            {
                _startDateSelected = DateTime.Today;
                _endDateSelected = DateTime.Today;
            }
            IsStartEndDateValid = true;

            CancelCommand = new Command(OnCancelClicked);
            NextCommand = new Command(OnNextClicked, CanNextClicked);
        }

        private void InitCourseData(Course course)
        {
            _title = course.Title;
            _startDateSelected = course.StartDate;
            _endDateSelected = course.EndDate;
            _selectedStatus = course.Status;
    }

        private bool CanNextClicked(object arg)
        {
            bool titleIsValid = !string.IsNullOrWhiteSpace(_title);
            bool StartEndValid = _startDateSelected <= _endDateSelected;   
            return titleIsValid && StartEndValid && IsStatusSelected;
        }

        private async void OnNextClicked(object obj)
        {
            _course.Title = Title;
            _course.StartDate = StartDateSelected;
            _course.EndDate = EndDateSelected;
            _course.Status = SelectedStatus;
            await App.Current.MainPage.Navigation.PushAsync(new NewCoursePage2(_course, _term));
        }

        private async void OnCancelClicked(object obj)
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
