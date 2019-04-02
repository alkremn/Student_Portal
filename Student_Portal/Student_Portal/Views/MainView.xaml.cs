using Student_Portal.ViewModels;
using Xamarin.Forms;

namespace Student_Portal.View
{
    public partial class MainView : ContentPage
    {
        public MainView()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }
    }
}
