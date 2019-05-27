using Student_Portal.Models;
using Student_Portal.Services;
using Student_Portal.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Student_Portal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseDetailPage : ContentPage
    {
        public CourseDetailPage(Course course, AssessmentDataService assessmentDS)
        {
            InitializeComponent();
            BindingContext = new CourseDetailPageViewModel(course, assessmentDS);

            AssessmentList.ItemSelected += (sender, e) =>
            {
                ((ListView)sender).SelectedItem = null;
            };
        }
    }
}