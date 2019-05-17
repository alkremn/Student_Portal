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
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string SelectedStatus { get; private set; }
        public List<string> StatusOptions { get; }

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

        public NewCoursePage1ViewModel()
        {
            StatusOptions = new List<string>()
            {
                "In Progress",
                "Completed",
                "Dropped",
                "Plan To Take"
            };
            CancelCommand = new Command(OnCancelClicked);
            NextCommand = new Command(OnNextClicked, CanNextClicked);
        }

        private async void OnNextClicked(object obj)
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new NewCoursePage2());
        }

        private bool CanNextClicked(object obj)
        {
            if (title != null)
                return title.Length != 0;
            else
                return false;
        }

        private async void OnCancelClicked(object obj)
        {
            await App.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}
