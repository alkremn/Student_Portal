using Student_Portal.ViewModel;
using Xamarin.Forms;

namespace Student_Portal.View
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
