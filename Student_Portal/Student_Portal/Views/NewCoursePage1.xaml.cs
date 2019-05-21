using Student_Portal.Services;
using Student_Portal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Student_Portal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewCoursePage1 : ContentPage
    {
        private bool isTitleEntered = false;
        private bool isStartDateSelected = false;
        private bool isEndDateSeleced = false;
        private bool isStatusSelected = false;

        public NewCoursePage1(int termId)
        {
            InitializeComponent();
            BindingContext = new NewCoursePage1ViewModel(termId);
            NextButton.IsEnabled = false;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private void Title_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry title = sender as Entry;
            if(title != null)
            {
                if (title.Text.Length != 0)
                    isTitleEntered = true;
                else
                    isTitleEntered = false;

                if(isTitleEntered && isStartDateSelected && isEndDateSeleced && isStatusSelected)
                {
                    NextButton.IsEnabled = true;
                }
                else
                {
                    NextButton.IsEnabled = false;
                }
            }
        }

        private void StartDate_Selected(object sender, DateChangedEventArgs e)
        {
            isStartDateSelected = true;

            if (isTitleEntered && isStartDateSelected && isEndDateSeleced && isStatusSelected)
            {
                NextButton.IsEnabled = true;
            }
            else
            {
                NextButton.IsEnabled = false;
            }
        }
        private void EndDate_Selected(object sender, DateChangedEventArgs e)
        {
            isEndDateSeleced = true;

            if (isTitleEntered && isStartDateSelected && isEndDateSeleced && isStatusSelected)
            {
                NextButton.IsEnabled = true;
            }
            else
            {
                NextButton.IsEnabled = false;
            }
        }

        private void StatusIndexChanged(object sender, EventArgs e)
        {
            isStatusSelected = true;

            if (isTitleEntered && isStartDateSelected && isEndDateSeleced && isStatusSelected)
            {
                NextButton.IsEnabled = true;
            }
            else
            {
                NextButton.IsEnabled = false;
            }
        }
    }
}