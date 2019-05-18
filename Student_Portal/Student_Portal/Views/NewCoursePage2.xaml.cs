using Student_Portal.Models;
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
        private bool isInsNameEntered = false;
        private bool isPhoneEntered = false;
        private bool isEmailEntered = false;
        private const string P1 = "Name";
        private const string P2 = "Phone";
        private const string P3 = "Email";

        public NewCoursePage2(Course newCourse)
        {
            InitializeComponent();
            BindingContext = new NewCoursePage2ViewModel(newCourse);
            NextButton.IsEnabled = false;
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = sender as Entry;
            string placeHolder = entry.Placeholder;
            switch (placeHolder)
            {
                case P1:
                    isInsNameEntered = true;
                    break;
                case P2:
                    isPhoneEntered = true;
                    break;
                case P3:
                    isEmailEntered = true;
                    break;
            }

            if (isInsNameEntered && isPhoneEntered && isEmailEntered)
                NextButton.IsEnabled = true;
        }
      
    }
}