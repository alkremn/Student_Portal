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
        private CourseDataService _courseDS;
        private int termId;
        private const string NEW_COURSE_SAVED = "new_course_saved";
        private AssessmentDataService assessmentDS;

        public string Title { get; set; }
        public ObservableCollection<Course> Courses { get; } 
        public ICommand AddNewCourseCommand { get; }
        public ICommand ModifyCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand BackCommand { get; }
        public Course SelectedCourse { get; }

        public TermDetailViewModel(CourseDataService courseDS, Term term)
        {
            _courseDS = courseDS;
            Title = term.Title;
            termId = term.Id;
            Courses = new ObservableCollection<Course>();
            assessmentDS = new AssessmentDataService(App.Database);
            LoadCourseData();
            AddNewCourseCommand = new Command(OnNewCourseCreate);
            ModifyCommand = new Command(async (obj) => await OnModifyClicked(obj));
            DeleteCommand = new Command(async (obj) => await OnDeleteClicked(obj));
            BackCommand = new Command(OnBackClicked);
            MessagingCenter.Subscribe<Course>(this, NEW_COURSE_SAVED, async (course) => await NewCourseSaved(course));
        }

        private async Task NewCourseSaved(Course course)
        {
            if (course == null)
                return;

            await _courseDS.SaveCourseAsync(course);
            LoadCourseData();
        }

        private async void OnBackClicked(object obj)
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private async Task OnDeleteClicked(object obj)
        {
            if (obj == null)
                return;
            Course course = obj as Course;
            await assessmentDS.DeleteAssessmentsByCourseIdAsync(course.Id);
            await _courseDS.DeleteCourseAsync(course);
            LoadCourseData();
        }

        private Task OnModifyClicked(object obj)
        {
            //TODO: Implement this method
            throw new NotImplementedException();
        }

        private async void LoadCourseData()
        {
            var courses =  await _courseDS.GetAllCoursesByTermIdAsync(termId);
            Courses.Clear();
            courses.ToList().ForEach(c => Courses.Add(c));
        }

        private async void OnNewCourseCreate()
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new NewCoursePage1(termId));
        }
    }
}
