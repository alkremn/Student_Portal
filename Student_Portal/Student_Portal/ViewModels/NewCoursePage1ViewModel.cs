using Student_Portal.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.LocalNotifications;
using Student_Portal.Models;

namespace Student_Portal.ViewModels
{
    public class NewCoursePage1ViewModel:BaseViewModel
    {
        private string title;
        private int termNumber;
        public DateTime StartDateSelected { get; private set; }
        public DateTime EndDateSelected { get; private set; }
        public string SelectedStatus { get; private set; }

        public ICommand CancelCommand { get; }
        public ICommand NextCommand { get; }

        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public NewCoursePage1ViewModel(int termId)
        {
            this.termNumber = termId;

            CancelCommand = new Command(OnCancelClicked);
            NextCommand = new Command(OnNextClicked);
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
