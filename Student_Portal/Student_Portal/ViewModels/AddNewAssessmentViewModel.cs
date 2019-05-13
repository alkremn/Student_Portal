using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Student_Portal.ViewModels
{
    public class AddNewAssessmentViewModel
    {
        public ICommand CancelCommand { get; }

        public AddNewAssessmentViewModel()
        {
            CancelCommand = new Command(OnCancelClicked);
        }

        private async void OnCancelClicked(object obj)
        {
            await App.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}
