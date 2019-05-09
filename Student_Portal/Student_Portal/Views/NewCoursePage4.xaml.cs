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
        public NewCoursePage4()
        {
            InitializeComponent();
            BindingContext = new NewCoursePage4ViewModel();
        }
    }
}