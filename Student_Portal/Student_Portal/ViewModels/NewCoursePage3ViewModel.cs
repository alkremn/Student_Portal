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
        private Course _course;
        private Term _term;

        public string Notes { get; set; }
        public ICommand PrevCommand { get; }
        public ICommand NextCommand { get; }

        public NewCoursePage3ViewModel(Course course, Term term)
        {
            _course = course;
            _term = term;
            if (course.IsExisting)
                InitCourseData(_course);

            PrevCommand = new Command(OnPrevClicked);
            NextCommand = new Command(OnNextClicked);
        }

        private void InitCourseData(Course course)
        {
            if (course.Notes != null)
                Notes = course.Notes;
        }

        private async void OnPrevClicked(object obj)
        {
            
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private async void OnNextClicked(object obj)
        {
            _course.Notes = Notes;
            await App.Current.MainPage.Navigation.PushAsync(new NewCoursePage4(_course, _term));
        }
    }
}
