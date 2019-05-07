using Student_Portal.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Student_Portal.ViewModels
{
    public class NewCoursePage1ViewModel
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<string> StatusOptions { get; }
        public string SelectedStatus { get; set; }
        public ICommand CancelCommand { get; }
        public ICommand NextCommand { get; }

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
            NextCommand = new Command(OnNextClicked);
        }

        private async void OnNextClicked(object obj)
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new NewCoursePage2());
        }

        private async void OnCancelClicked(object obj)
        {
           await App.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}
