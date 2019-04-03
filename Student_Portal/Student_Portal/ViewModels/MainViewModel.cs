﻿using Student_Portal.Models;
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

            AddNewTermCommand = new Command(OnTermCreate);
            RefreshingCommand = new Command(LoadTerms);
            ModifyCommand = new Command(async (obj) => await OnModifyClicked(obj));
            DeleteCommand = new Command(async (obj) => await OnDeleteClicked(obj));

            MessagingCenter.Subscribe<NewTermViewModel>(this, App.saved, OnSaveClicked);
        }

        private async void OnTermCreate()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new NewTermPage(null));
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

            await Application.Current.MainPage.Navigation.PushAsync(new NewTermPage(term));
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
