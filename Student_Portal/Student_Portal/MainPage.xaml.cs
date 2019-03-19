using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Student_Portal
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            image.Source = ImageSource.FromResource("Student_Portal.Images.DegreeIcon.png");
        }
    }
}
