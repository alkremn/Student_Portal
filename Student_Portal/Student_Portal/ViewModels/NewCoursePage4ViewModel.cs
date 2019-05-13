using Student_Portal.Models;
using System;
using Student_Portal.Services.SampleData;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Student_Portal.Views;

namespace Student_Portal.ViewModels
{
    public class NewCoursePage4ViewModel
    {
        public ObservableCollection<Assessment> Assessments { get; }
        public ICommand AddNewAssessCommand { get; }
        public ICommand PrevCommand { get; }
        public ICommand SaveCommand { get; }
        public NewCoursePage4ViewModel()
        {
            Assessments = new ObservableCollection<Assessment>();
            AddNewAssessCommand = new Command(OnAddNewAssessment);
            PrevCommand = new Command(OnPrevClicked);
            SaveCommand = new Command(OnSaveClicked);
            LoadData();
        }

        private async void OnAddNewAssessment(object obj)
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new AddNewAssessmentPage());
        }

        private async void OnPrevClicked(object obj)
        {
            await App.Current.MainPage.Navigation.PopModalAsync();
        }

        private async void OnSaveClicked(object obj)
        {

        }

        private void LoadData()
        {
            var assessments = MockAssessmentRepository.GetSampleAssessmentList();
            assessments.ToList().ForEach(a => Assessments.Add(a));
        }
    }
}
