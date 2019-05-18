using Student_Portal.Models;
using Student_Portal.Services;
using Student_Portal.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Student_Portal.ViewModels
{
    public class TermDetailViewModel : BaseViewModel
    {
        private CourseDataService _courseData;
        public string Title { get; set; }
        private int termId;
        public ObservableCollection<Course> Courses { get; } 
        public ICommand AddNewCourseCommand { get; }
        public ICommand ModifyCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand BackCommand { get; }
        public Course SelectedCourse { get; }

        public TermDetailViewModel(CourseDataService courseData, Term term)
        {
            _courseData = courseData;
            Title = term.Title;
            termId = term.Id;
            Courses = new ObservableCollection<Course>();
            LoadCourseData();
            AddNewCourseCommand = new Command(OnNewCourseCreate);
            ModifyCommand = new Command(async (obj) => await OnModifyClicked(obj));
            DeleteCommand = new Command(async (obj) => await OnDeleteClicked(obj));
            BackCommand = new Command(OnBackClicked);
        }

        private async void OnBackClicked(object obj)
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private Task OnDeleteClicked(object obj)
        {
            throw new NotImplementedException();
        }

        private Task OnModifyClicked(object obj)
        {
            throw new NotImplementedException();
        }

        private void LoadCourseData()
        {
            var courses = MockCourseRepository.GetCourseList();
            Courses.Clear();
            courses.ToList().ForEach(c => Courses.Add(c));
        }

        private async void OnNewCourseCreate()
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new NewCoursePage1(termId));
        }
    }
}
