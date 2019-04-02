using Student_Portal.Model;
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
	public partial class TermDetailView : ContentPage
	{
		public TermDetailView (Term term)
		{
			InitializeComponent ();
            BindingContext = new TermDetailViewModel(term);
		}
    }
}