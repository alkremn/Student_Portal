using Student_Portal.Model;
using Student_Portal.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Student_Portal.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewTermPage : ContentPage
	{

		public NewTermPage (Term term)
		{
			InitializeComponent ();
            BindingContext = new NewTermViewModel(term);
		}

        //disables back button on Android
        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}