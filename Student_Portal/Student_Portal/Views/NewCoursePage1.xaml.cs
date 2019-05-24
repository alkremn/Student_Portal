using Student_Portal.Models;
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
        public NewCoursePage1(Course course)
        {
            InitializeComponent();
            BindingContext = new NewCoursePage1ViewModel(course);
            NextButton.IsEnabled = false;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}