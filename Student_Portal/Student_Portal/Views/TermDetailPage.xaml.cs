﻿using Student_Portal.Models;
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
	public partial class TermDetailPage : ContentPage
	{
		public TermDetailPage (CourseDataService courseData, Term selectedTerm)
		{
			InitializeComponent ();
            BindingContext = new TermDetailViewModel(courseData, selectedTerm);

            CourseList.ItemSelected += (sender, e) =>
            {
                ((ListView)sender).SelectedItem = null;
            };

		}
    }
}