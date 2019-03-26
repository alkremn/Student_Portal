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
		public NewTermPage ()
		{
			InitializeComponent ();
            BindingContext = new NewTermViewModel();
		}
	}
}