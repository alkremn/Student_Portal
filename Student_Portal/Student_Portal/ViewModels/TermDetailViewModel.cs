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
        public ObservableCollection<Course> Courses { get; private set; } 
        public ICommand AddNewCourseCommand { get; private set; }
        public ICommand ModifyCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public Course SelectedCourse { get; private set; }

        public TermDetailViewModel(CourseDataService courseData, Term term)
        {
            _courseData = courseData;
            Title = term.Title;
            Courses = new ObservableCollection<Course>();
            LoadCourseData();
            AddNewCourseCommand = new Command(OnNewCourseCreate);
            ModifyCommand = new Command(async (obj) => await OnModifyClicked(obj));
            DeleteCommand = new Command(async (obj) => await OnDeleteClicked(obj));
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
            await App.Current.MainPage.Navigation.PushModalAsync(new NewCoursePage1());
        }
    }
}
