﻿using Student_Portal.Models;
using System;
using Student_Portal.Services.SampleData;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Student_Portal.Views;
using System.Threading.Tasks;
using Student_Portal.Services;

namespace Student_Portal.ViewModels
{
    public class NewCoursePage4ViewModel : BaseViewModel
    {
        private Course course;
        private AssessmentDataService assessmentDS;
        private string title;
        private Assessment selectedAssessment;

        public ObservableCollection<Assessment> Assessments { get; }
        public ICommand AddNewAssessmentCommand { get; }
        public ICommand ModifyAssessmentCommand { get; }
        public ICommand DeleteAssessmentCommand { get; }
        public ICommand PrevCommand { get; }
        public ICommand SaveCommand { get; }

        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged();
            }
        }

        public Assessment SelectedAssessment
        {
            get => selectedAssessment;
            set
            {
                selectedAssessment = value;
                OnPropertyChanged();
                if (value != null)
                    LoadDetailPage(value);
            }
        }

        public NewCoursePage4ViewModel(Course newCourse)
        {
            course = newCourse;
            assessmentDS = new AssessmentDataService(App.Database);
            Assessments = new ObservableCollection<Assessment>();
            AddNewAssessmentCommand = new Command(OnAddNewAssessment, CanAddNewAssessment);
            ModifyAssessmentCommand = new Command(async (obj) => await OnModifyAssessmentClicked(obj));
            DeleteAssessmentCommand = new Command(async (obj) => await OnDeleteAssessmentClicked(obj));

            PrevCommand = new Command(OnPrevClicked);
            SaveCommand = new Command(OnSaveClicked);
            MessagingCenter.Subscribe<Assessment>(this, "Created", async(obj) => await OnAssessmentSave(obj));
            LoadData();
        }

        private async Task OnAssessmentSave(Assessment assessment)
        {
            if(assessment != null)
                await assessmentDS.SaveAssessmentAsync(assessment);

            LoadData();
        }

        private async Task OnModifyAssessmentClicked(object obj)
        {
            if (obj == null)
                return;
            Assessment assessment = obj as Assessment;
            await App.Current.MainPage.Navigation.PushModalAsync(new AddNewAssessmentPage(assessmentDS, assessment, course.Id));
        }

        private async Task OnDeleteAssessmentClicked(object obj)
        {
            if (obj == null)
                return;
            Assessment assessment = obj as Assessment;
            await assessmentDS.DeleteAssessmentAsync(assessment);
            Assessments.Remove(assessment);
        }

        private bool CanAddNewAssessment(object arg)
        {
            return Assessments.Count < 2;
        }

        private async void OnAddNewAssessment(object obj)
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new AddNewAssessmentPage(assessmentDS, null, course.Id));
        }

        private async void OnPrevClicked(object obj)
        {
            await App.Current.MainPage.Navigation.PopModalAsync();
        }

        private async void OnSaveClicked(object obj)
        {

        }

        private async void LoadDetailPage(Assessment assessment)
        {
            selectedAssessment = null;
            await App.Current.MainPage.Navigation.PushModalAsync(new AddNewAssessmentPage(assessmentDS, assessment, course.Id));
        }

        private async void LoadData()
        {
            var assessments = await assessmentDS.GetAllAssessmentsByCourseIdAsync(course.Id);
            assessments.Clear();
            assessments.ToList().ForEach(a => Assessments.Add(a));
        }
    }
}
