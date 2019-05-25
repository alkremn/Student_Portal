using Student_Portal.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Student_Portal.Views;
using System.Threading.Tasks;
using Student_Portal.Services;

namespace Student_Portal.ViewModels
{
    public class NewCoursePage4ViewModel : BaseViewModel
    {
        private Course _course;
        private Term _term;
        private AssessmentDataService _assessmentDS;
        private string _title;

        private const string COUNT_LESS_TWO = "count<2";
        private const string COUNT_EQ_TWO = "count=2";
        private const string NEW_COURSE_SAVED = "new_course_saved";
        private const string CREATED = "created";

        public ObservableCollection<Assessment> Assessments { get; }
        public ICommand AddNewAssessmentCommand { get; }
        public ICommand ModifyAssessmentCommand { get; }
        public ICommand DeleteAssessmentCommand { get; }
        public ICommand PrevCommand { get; }
        public ICommand SaveCommand { get; }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public NewCoursePage4ViewModel(Course course, Term term)
        {
            _course = course;
            _term = term;
            _assessmentDS = new AssessmentDataService(App.Database);
            Assessments = new ObservableCollection<Assessment>();
            AddNewAssessmentCommand = new Command(OnAddNewAssessment);
            ModifyAssessmentCommand = new Command(async (obj) => await OnModifyAssessmentClicked(obj));
            DeleteAssessmentCommand = new Command((obj) => OnDeleteAssessmentClicked(obj));

            PrevCommand = new Command(OnPrevClicked);
            SaveCommand = new Command(OnSaveClicked);
            MessagingCenter.Subscribe<Assessment>(this, CREATED, (obj) => OnAssessmentSave(obj));

            if (_course.IsExisting)
                InitCourseData(course);
        }

        private async void InitCourseData(Course course)
        {
            var assessments = await _assessmentDS.GetAllAssessmentsByCourseIdAsync(course.Id);
            Assessments.Clear();
            assessments.ToList().ForEach(a => Assessments.Add(a));
            CheckAssessmentListCount(Assessments);
        }

        private void OnAssessmentSave(Assessment assessment)
        {
            if (assessment != null)
                Assessments.Add(assessment);
            CheckAssessmentListCount(Assessments);
        }

        private async Task OnModifyAssessmentClicked(object obj)
        {
            if (obj == null)
                return;
            Assessment assessment = obj as Assessment;
            await App.Current.MainPage.Navigation.PushAsync(new AddNewAssessmentPage(_assessmentDS, assessment, Assessments.ToList(), _course.Id));
        }

        private async void OnDeleteAssessmentClicked(object obj)
        {
            if (obj == null)
                return;
            Assessment assessment = obj as Assessment;
            await _assessmentDS.DeleteAssessmentAsync(assessment);
            Assessments.Remove(assessment);
            CheckAssessmentListCount(Assessments);
        }

        private async void OnAddNewAssessment(object obj)
        {
            await App.Current.MainPage.Navigation.PushAsync(new AddNewAssessmentPage(_assessmentDS, null, Assessments.ToList(), _course.Id));
        }

        private async void OnPrevClicked(object obj)
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private async void OnSaveClicked(object obj)
        {
            _course.IsExisting = true;
            await _assessmentDS.SaveAssessmentListAsync(Assessments.ToList());

            MessagingCenter.Send(_course, NEW_COURSE_SAVED);
            await Application.Current.MainPage.Navigation.PushAsync(new TermDetailPage(new CourseDataService(App.Database), _term));
        }

        private async void LoadDetailPage(Assessment assessment)
        {
            await App.Current.MainPage.Navigation.PushAsync(new AddNewAssessmentPage(_assessmentDS, assessment, Assessments.ToList(), _course.Id));
        }

        private void CheckAssessmentListCount(ObservableCollection<Assessment> list)
        {
            if (list.Count < 2)
            {
                MessagingCenter.Send(this, COUNT_LESS_TWO);
            }
            else
            {
                MessagingCenter.Send(this, COUNT_EQ_TWO);
            }
        }
    }
}
