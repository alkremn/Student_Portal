using Student_Portal.ViewModels;
using Xamarin.Forms;

namespace Student_Portal.Views
{
    public partial class MainView : ContentPage
    {
        public MainView(IMainViewModel mainVM)
        {
            InitializeComponent();
            BindingContext = mainVM;
        }
    }
}
