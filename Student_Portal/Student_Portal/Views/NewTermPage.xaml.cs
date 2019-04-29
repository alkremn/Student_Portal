using Student_Portal.Models;
using Student_Portal.Services;
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
	public partial class NewTermPage : ContentPage
	{
		public NewTermPage (TermDataService termData, Term term)
		{
			InitializeComponent();
            BindingContext = new NewTermViewModel(termData, term);
		}
        public NewTermPage() { }
    }
}