using Student_Portal.Models;
using Student_Portal.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Student_Portal.ViewModels
{
    public class NewCoursePage2ViewModel
    {
        private Course course;
        public string InsName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public ICommand PrevCommand { get; }
        public ICommand NextCommand { get; }

        public NewCoursePage2ViewModel(Course newCourse)
        {
            course = newCourse;
            PrevCommand = new Command(OnPrevClicked);
            NextCommand = new Command(OnNextClicked);
        }

        private async void OnNextClicked(object obj)
        {
            course.InstructorName = InsName;
            course.InstructorPhone = Phone;
            course.InstructorEmail = Email;
            await App.Current.MainPage.Navigation.PushModalAsync(new NewCoursePage3(course));
        }

        private async void OnPrevClicked(object obj)
        {
            await App.Current.MainPage.Navigation.PopModalAsync();
        }

    }
}
