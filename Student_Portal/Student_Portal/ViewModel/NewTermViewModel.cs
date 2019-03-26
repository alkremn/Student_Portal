using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Student_Portal.ViewModel
{
    class NewTermViewModel
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public NewTermViewModel()
        {
            SaveCommand = new Command(async () => await OnNewTermSave());
            CancelCommand = new Command(async () => await OnCancelClicked());
        }

        private async Task OnNewTermSave()
        {
            throw new NotImplementedException();
        }
        private async Task OnCancelClicked()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
