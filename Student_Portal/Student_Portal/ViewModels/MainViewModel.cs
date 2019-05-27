using Student_Portal.Models;
using Student_Portal.Services;
using Student_Portal.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Student_Portal.ViewModels
{
    public class MainViewModel : BaseViewModel, IMainViewModel
    {
        private Term _selectedTerm;
        private TermDataService _termData;
        private CourseDataService _courseData;
        private AssessmentDataService _assessmentData;
        public ObservableCollection<Term> Terms { get; }
        public ICommand AddNewTermCommand { get; }
        public ICommand RefreshingCommand { get; }
        public ICommand ModifyCommand { get; }
        public ICommand DeleteCommand { get; }

        public Term SelectedTerm
        {
            get => _selectedTerm;
            set
            {
                 _selectedTerm = value;
                 OnPropertyChanged();

                if (value != null)
                    LoadTermDetailPage(value);
            }
        }

        public MainViewModel()
        {
            Terms = new ObservableCollection<Term>();
            _termData = new TermDataService(App.Database);
            _courseData = new CourseDataService(App.Database);
            _assessmentData = new AssessmentDataService(App.Database);
            InitTermData();
            AddNewTermCommand = new Command(OnTermCreate);
            RefreshingCommand = new Command(LoadTermData);
            ModifyCommand = new Command(async (obj) => await OnModifyClicked(obj));
            DeleteCommand = new Command(async (obj) => await OnDeleteClicked(obj));

            MessagingCenter.Subscribe<NewTermViewModel>(this, App.saved, OnSaveClicked);
        }

        private async void OnTermCreate()
        {

            await Application.Current.MainPage.Navigation.PushAsync(new NewTermPage(_termData, null));
        }

        private void OnSaveClicked(NewTermViewModel obj)
        {
            LoadTermData();
        }

        private async Task OnModifyClicked(object obj)
        {
            if (obj == null)
                return;
            Term term = (Term)obj;

            await Application.Current.MainPage.Navigation.PushAsync(new NewTermPage(_termData,term));
        }

        private async Task OnDeleteClicked(object obj)
        {
            if (obj == null)
                return;

            Term term = (Term)obj;
            DeleteCoursesByTermId(term.Id);

            await _termData.DeleteTermAsync(term);
            LoadTermData();
        }
        private async void InitTermData()
        {
            await SampleDataRepository.SetMockData(_termData, _courseData, _assessmentData);
            LoadTermData();
        }

        private async void LoadTermDetailPage(Term selectedTerm)
        {
            SelectedTerm = null;
            await Application.Current.MainPage.Navigation.PushAsync(new TermDetailPage(_courseData, selectedTerm));
        }

        private async void LoadTermData()
        {
            var terms = await _termData.GetTermsAsync();
            Terms.Clear();
            terms.ToList().ForEach(t => Terms.Add(t));
        }

        private async void DeleteCoursesByTermId(int termId)
        {
            var courses = await _courseData.GetAllCoursesByTermIdAsync(termId);
            foreach (var course in courses)
                await _assessmentData.DeleteAssessmentsByCourseIdAsync(course.Id);

            await _courseData.DeleteCoursesByTermIdAsync(termId);
        }




    }
}
