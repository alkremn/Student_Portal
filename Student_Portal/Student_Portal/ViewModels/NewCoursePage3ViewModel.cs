using Student_Portal.Models;
using Student_Portal.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Student_Portal.ViewModels
{
    public class NewCoursePage3ViewModel
    {
        private Course course;
        public string Notes { get; set; }
        public ICommand PrevCommand { get; }
        public ICommand NextCommand { get; }

        public NewCoursePage3ViewModel(Course newCourse)
        {
            course = newCourse;
            PrevCommand = new Command(OnPrevClicked);
            NextCommand = new Command(OnNextClicked);
        }

        private async void OnPrevClicked(object obj)
        {
            
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private async void OnNextClicked(object obj)
        {
            course.Notes = Notes;
            await App.Current.MainPage.Navigation.PushAsync(new NewCoursePage4(course));
        }
    }
}
