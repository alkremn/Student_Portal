using Student_Portal.Models;
using Student_Portal.Views;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Student_Portal.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<Term> Terms { get; private set; } = new ObservableCollection<Term>();
        public ICommand AddNewTermCommand { get; private set; }

        public MainViewModel()
        {
            AddNewTermCommand = new Command(async () => await OnTermCreate());
        }

        private async Task OnTermCreate()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new NewTermPage());
        }
    }
}
