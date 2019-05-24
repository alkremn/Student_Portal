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
        private int termNumber;

        private bool IsStartDateSelected = false;
        private bool IsEndDateSelected = false;
        private bool IsStatusSelected = false;

        public Command CancelCommand { get; }
        public Command NextCommand { get; }

        private string title;
        public string Title
        {
            get => title;
            set
            {
                title = value;
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

        public NewCoursePage1ViewModel(int termId)
        {
            termNumber = termId;

            CancelCommand = new Command(OnCancelClicked);
            NextCommand = new Command(OnNextClicked, CanNextClicked);
        }

        private bool CanNextClicked(object arg)
        {
            return !string.IsNullOrWhiteSpace(Title) && IsStartDateSelected && IsEndDateSelected && IsStatusSelected;
        }

        private async void OnNextClicked(object obj)
        {
            await App.Current.MainPage.Navigation.PushModalAsync(
                new NewCoursePage2(new Course()
                {
                    Title = title,
                    TermId = termNumber,
                    StartDate = StartDateSelected,
                    EndDate = EndDateSelected,
                    Status = SelectedStatus
                }));
        }

        private async void OnCancelClicked(object obj)
        {
            await App.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}
