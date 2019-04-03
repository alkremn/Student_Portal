using Student_Portal.ViewModels;
using Xamarin.Forms;

namespace Student_Portal.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }
    }
}
