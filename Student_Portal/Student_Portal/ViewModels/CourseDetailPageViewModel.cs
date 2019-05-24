using Student_Portal.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Student_Portal.ViewModels
{
    public class CourseDetailPageViewModel : BaseViewModel
    {
        private Course _selectedCourse;
        public Course SelectedCourse
        {
            get => _selectedCourse;
            set
            {
                _selectedCourse = value;
                OnPropertyChanged();
            }
        }

        public Command BackCommand { get; private set; }

        public CourseDetailPageViewModel(Course selectedCourse)
        {
            SelectedCourse = selectedCourse;
            BackCommand = new Command(OnBackClicked);
        }

        private async void OnBackClicked(object obj)
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}
