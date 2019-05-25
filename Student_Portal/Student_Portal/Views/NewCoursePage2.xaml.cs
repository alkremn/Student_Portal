using Student_Portal.Models;
using Student_Portal.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Student_Portal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewCoursePage2 : ContentPage
    {
        public NewCoursePage2(Course newCourse, Term term)
        {
            InitializeComponent();
            BindingContext = new NewCoursePage2ViewModel(newCourse, term);
            NextButton.IsEnabled = false;
        }
    }
}