using Student_Portal.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Student_Portal.ViewModels
{
    public class NewCoursePage3ViewModel
    {
        public ICommand NextCommand { get; }

        public NewCoursePage3ViewModel()
        {
            NextCommand = new Command(OnNextClicked);
        }

        private async void OnNextClicked(object obj)
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new NewCoursePage4());
        }
    }
}
