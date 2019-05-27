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
    public partial class CourseDetailPage : ContentPage
    {
        private const string COUNT_LESS_TWO = "count<2";
        private const string COUNT_EQ_TWO = "count=2";

        public CourseDetailPage(Course course, AssessmentDataService assessmentDS)
        {
            InitializeComponent();
            BindingContext = new CourseDetailPageViewModel(course, assessmentDS);

            AssessmentList.ItemSelected += (sender, e) =>
            {
                ((ListView)sender).SelectedItem = null;
            };

            MessagingCenter.Subscribe<CourseDetailPageViewModel>(this, COUNT_LESS_TWO, (sender) =>
            {
                AddAssessmentButton.IsVisible = true;
            });
            MessagingCenter.Subscribe<CourseDetailPageViewModel>(this, COUNT_EQ_TWO, (sender) =>
            {
                AddAssessmentButton.IsVisible = false;
            });


        }
    }
}