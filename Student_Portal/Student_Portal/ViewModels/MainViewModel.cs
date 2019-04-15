﻿using Student_Portal.Models;
using Student_Portal.Services;
using Student_Portal.Views;
using System;
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
        public ObservableCollection<Term> Terms { get; private set; }
        public ICommand AddNewTermCommand { get; private set; }
        public ICommand RefreshingCommand { get; private set; }
        public ICommand ModifyCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        public Term SelectedTerm
        {
            get => _selectedTerm;
            set
            {
                _selectedTerm = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            Terms = new ObservableCollection<Term>();
            LoadTerms();
            courseData = new CourseDataService(App.Database);
            termData = new TermDataService(App.Database);

            AddNewTermCommand = new Command(OnTermCreate);
            RefreshingCommand = new Command(LoadTerms);
            ModifyCommand = new Command(async (obj) => await OnModifyClicked(obj));
            DeleteCommand = new Command(async (obj) => await OnDeleteClicked(obj));

            MessagingCenter.Subscribe<NewTermViewModel>(this, App.saved, OnSaveClicked);
        }

        private async void OnTermCreate()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new NewTermPage(termData, null));
        }

        private void LoadTerms()
        {
            var terms = MockTermRepository.GetTermList(); 
            Terms.Clear();
            terms.ToList().ForEach(t => Terms.Add(t));
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

            await Application.Current.MainPage.Navigation.PushAsync(new NewTermPage(termData, term));
        }

        private async Task OnDeleteClicked(object obj)
        {
            if (obj == null)
                return;

            Term term = (Term)obj;
            await termData.DeleteTermAsync(term);
            LoadTerms();
        }
        private async void LoadDetailPage(Term selectedTerm)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new TermDetailPage(courseData, selectedTerm));
        }
    }
}
