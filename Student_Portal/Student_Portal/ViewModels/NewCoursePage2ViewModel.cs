﻿using Student_Portal.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Student_Portal.ViewModels
{
    public class NewCoursePage2ViewModel
    {
        public ICommand PrevCommand { get; }
        public ICommand NextCommand { get; }

        public NewCoursePage2ViewModel()
        {
            PrevCommand = new Command(OnPrevClicked);
            NextCommand = new Command(OnNextClicked);
        }

        private async void OnNextClicked(object obj)
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new NewCoursePage3());
        }

        private async void OnPrevClicked(object obj)
        {
            await App.Current.MainPage.Navigation.PopModalAsync();
        }

    }
}