using Student_Portal.Model;
using Student_Portal.View;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Student_Portal.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<Term> Terms { get; private set; }
        public ICommand AddNewTermCommand { get; private set; }

        public MainViewModel()
        {
            Terms = new ObservableCollection<Term>();
            LoadTerms();
            AddNewTermCommand = new Command(async () => await OnTermCreate());
        }

        private async Task OnTermCreate()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new NewTermPage());
        }

        private async void LoadTerms()
        {
            var terms = await App.Database.GetTermsAsync();
            foreach(Term term in terms)
            {
                Terms.Add(term);
            }
        }
    }
}
