using Student_Portal.Model;
using Student_Portal.View;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Student_Portal.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private Term _selectedTerm;
        public ObservableCollection<Term> Terms { get; private set; }
        public ICommand AddNewTermCommand { get; private set; }
        public ICommand RefreshingCommand { get; private set; }
        public ICommand ModifyCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        public Term SelectedTerm
        {
            get
            {
                return _selectedTerm;
            }
            set
            {
                if(value != null)
                {
                    if(_selectedTerm != value)
                    {
                        _selectedTerm = value;
                        LoadDetailPage(value);
                    }
                }
            }
        }

        public MainViewModel()
        {
            Terms = new ObservableCollection<Term>();
            LoadTerms();
            AddNewTermCommand = new Command(async () => await OnTermCreate());
            RefreshingCommand = new Command(() => LoadTerms());
            ModifyCommand = new Command(async (obj) => await OnModifyClicked(obj));
            DeleteCommand = new Command(async (obj) => await OnDeleteClicked(obj));

            MessagingCenter.Subscribe<NewTermViewModel>(this, App.saved, OnSaveClicked);
        }

        private async Task OnTermCreate()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new NewTermView(null));
        }

        private async void LoadTerms()
        {
            var terms = await App.Database.GetTermsAsync();
            Terms.Clear();
            foreach(Term term in terms)
            {
                Terms.Add(term);
            }
        }

        private void OnSaveClicked(NewTermViewModel obj)
        {
            LoadTerms();
        }

        private async Task OnModifyClicked(object obj)
        {
            if (obj == null)
                return;
            Term term = (Term)obj;

            await Application.Current.MainPage.Navigation.PushAsync(new NewTermView(term));
        }

        private async Task OnDeleteClicked(object obj)
        {
            if (obj == null)
                return;

            Term term = (Term)obj;

            await App.Database.DeleteTermAsync(term);
            LoadTerms();
        }
        private async void LoadDetailPage(Term selectedTerm)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new TermDetailPage(selectedTerm));
        }
    }
}
