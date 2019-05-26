using Student_Portal.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Student_Portal.ViewModels
{
    public class CourseDetailPageViewModel : BaseViewModel
    {
        public string Title { get; set; }

        public Command BackCommand { get; private set; }

        public CourseDetailPageViewModel(Course selectedCourse)
        {
            Title = selectedCourse.Title;
            BackCommand = new Command(OnBackClicked);
        }

        private async void OnBackClicked(object obj)
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
