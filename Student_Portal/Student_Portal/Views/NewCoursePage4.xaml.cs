using Student_Portal.Models;
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
    public partial class NewCoursePage4 : ContentPage
    {

        private const string COUNT_LESS_TWO = "count<2";
        private const string COUNT_EQ_TWO = "count=2";

        public NewCoursePage4(Course course)
        {
            InitializeComponent();
            BindingContext = new NewCoursePage4ViewModel(course);
            AssessmentList.ItemSelected += (sender, e) =>
            {
                ((ListView)sender).SelectedItem = null;
            };
            MessagingCenter.Subscribe<NewCoursePage4ViewModel>(this, COUNT_LESS_TWO, (sender) => 
            {
                AddAssessmentButton.IsVisible = true;
            });
            MessagingCenter.Subscribe<NewCoursePage4ViewModel>(this, COUNT_EQ_TWO, (sender) =>
            {
                AddAssessmentButton.IsVisible = false;
            });

        }

    }
}