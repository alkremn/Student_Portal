using Student_Portal.Views;
using System;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.LocalNotifications;
using Student_Portal.Models;

namespace Student_Portal.ViewModels
{
    public class NewCoursePage1ViewModel:BaseViewModel
    {
        private bool IsStartDateSelected = false;
        private bool IsEndDateSelected = false;
        private bool IsStatusSelected = false;
        private Course course;

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

        private DateTime _startDateSelected;
        public DateTime StartDateSelected
        {
            get => _startDateSelected;
            set
            {
                _startDateSelected = value;
                IsStartDateSelected = true;
                OnPropertyChanged();
                NextCommand.ChangeCanExecute();
            }
        }

        private DateTime _endDateSelected;
        public DateTime EndDateSelected
        {
            get => _endDateSelected;
            set
            {
                _endDateSelected = value;
                IsEndDateSelected = true;
                OnPropertyChanged();
                NextCommand.ChangeCanExecute();
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

        public NewCoursePage1ViewModel(Course selectedCourse)
        {
            course = selectedCourse;

            if (course.IsExisting)
                InitCourseData(course);

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
            return !string.IsNullOrWhiteSpace(Title) && IsStartDateSelected && IsEndDateSelected && IsStatusSelected;
        }

        private async void OnNextClicked(object obj)
        {
            course.Title = Title;
            course.StartDate = StartDateSelected;
            course.EndDate = EndDateSelected;
            course.Status = SelectedStatus;
            await App.Current.MainPage.Navigation.PushAsync(new NewCoursePage2(course));
        }

        private async void OnCancelClicked(object obj)
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
