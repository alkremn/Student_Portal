using Student_Portal.Models;
using Student_Portal.Services;
using Student_Portal.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace Student_Portal.ViewModels
{
    public class NewCoursePage3ViewModel
    {
        private Course _course;
        private Term _term;
        private CourseDataService _courseDS;

        public string Notes { get; set; }
        public ICommand PrevCommand { get; }
        public ICommand SaveCommand { get; }

        public NewCoursePage3ViewModel(Course course, Term term)
        {
            _course = course;
            _term = term;
            if (course.IsExisting)
                InitCourseData(_course);

            _courseDS = new CourseDataService(App.Database);
            PrevCommand = new Command(OnPrevClicked);
            SaveCommand = new Command(OnSaveClicked);
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

        private async void OnSaveClicked(object obj)
        {
            _course.Notes = Notes;
            _course.IsExisting = true;
            await _courseDS.SaveCourseAsync(_course);
            await Application.Current.MainPage.Navigation.PushAsync(new TermDetailPage(_courseDS, _term));
        }
    }
}
