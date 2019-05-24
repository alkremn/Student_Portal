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
    public class MainViewModel : BaseViewModel, IMainViewModel
    {
        private Term _selectedTerm;
        private TermDataService termData;
        private CourseDataService courseData;
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
            termData = new TermDataService(App.Database);
            courseData = new CourseDataService(App.Database);
            LoadTermData();

            AddNewTermCommand = new Command(OnTermCreate);
            RefreshingCommand = new Command(LoadTermData);
            ModifyCommand = new Command(async (obj) => await OnModifyClicked(obj));
            DeleteCommand = new Command(async (obj) => await OnDeleteClicked(obj));

            MessagingCenter.Subscribe<NewTermViewModel>(this, App.saved, OnSaveClicked);
        }

        private async void OnTermCreate()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new NewTermPage(termData, null));
        }

        private async void LoadTermData()
        {
            var terms = await termData.GetTermsAsync();
            Terms.Clear();
            terms.ToList().ForEach(t => Terms.Add(t));
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

            await Application.Current.MainPage.Navigation.PushAsync(new NewTermPage(termData,term));
        }

        private async Task OnDeleteClicked(object obj)
        {
            if (obj == null)
                return;

            Term term = (Term)obj;
            await termData.DeleteTermAsync(term);
            LoadTermData();
        }
        private async void LoadTermDetailPage(Term selectedTerm)
        {
            SelectedTerm = null;
            await Application.Current.MainPage.Navigation.PushAsync(new TermDetailPage(courseData, selectedTerm));
        }
    }
}
