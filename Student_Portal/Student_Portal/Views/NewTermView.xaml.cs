using Student_Portal.Model;
using Student_Portal.ViewModels;
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
	public partial class NewTermView : ContentPage
	{
		public NewTermView (Term term)
		{
			InitializeComponent ();
            BindingContext = new NewTermViewModel(term);
		}
    }
}