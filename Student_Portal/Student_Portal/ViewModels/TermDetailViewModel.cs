using Student_Portal.Models;
using Student_Portal.Services;
using Student_Portal.Views;
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
        private Term _term;
        private AssessmentDataService _assessmentDS;
        private Course _selectedCourse;

        public string Title { get; set; }
        public Course SelectedCourse
        {
            get => _selectedCourse;
            set
            {
                _selectedCourse = value;
                OnPropertyChanged();

                if (value != null)
                    LoadCourseDetailPage(value);
            }
        }

        public ObservableCollection<Course> Courses { get; } 
        public ICommand AddNewCourseCommand { get; }
        public ICommand ModifyCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand BackCommand { get; }
        
        //Constructor
        public TermDetailViewModel(CourseDataService courseDS, Term term)
        {
            _courseDS = courseDS;
            _term = term;
            Title = term.Title;
            Courses = new ObservableCollection<Course>();
            _assessmentDS = new AssessmentDataService(App.Database);
            LoadCourseData();
            AddNewCourseCommand = new Command(OnNewCourseCreate);
            ModifyCommand = new Command(async (obj) => await OnModifyClicked(obj));
            DeleteCommand = new Command(async (obj) => await OnDeleteClicked(obj));
            BackCommand = new Command(OnBackClicked);
        }

        //Loads course detial page
        private async void LoadCourseDetailPage(Course selectedCourse)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new CourseDetailPage(selectedCourse, _assessmentDS));
        }

        private async void OnBackClicked(object obj)
        {
            await Application.Current.MainPage.Navigation.PopToRootAsync();
        }

        //Deletes course and all its assessments 
        private async Task OnDeleteClicked(object obj)
        {
            if (obj == null)
                return;
            Course course = obj as Course;

            await _assessmentDS.DeleteAssessmentsByCourseIdAsync(course.Id);
            await _courseDS.DeleteCourseAsync(course);
            LoadCourseData();
        }

        //Modifies selected course
        private async Task OnModifyClicked(object obj)
        {
            if (obj == null)
                return;
            Course selectedCourse = obj as Course;
            await Application.Current.MainPage.Navigation.PushAsync(new NewCoursePage1(selectedCourse, _term));
        }

        //Loads courses from database
        private async void LoadCourseData()
        {
            var courses =  await _courseDS.GetAllCoursesByTermIdAsync(_term.Id);
            Courses.Clear();
            courses.ToList().ForEach(c => Courses.Add(c));
        }

        //Loads new course page
        private async void OnNewCourseCreate()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new NewCoursePage1(new Course() { TermId = _term.Id }, _term));
        }
    }
}
