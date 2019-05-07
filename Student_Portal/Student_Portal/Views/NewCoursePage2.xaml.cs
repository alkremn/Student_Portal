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
    public partial class NewCoursePage2 : ContentPage
    {
        public NewCoursePage2()
        {
            InitializeComponent();
            BindingContext = new NewCoursePage2ViewModel();
        }
    }
}