using Student_Portal.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Student_Portal.ViewModels
{
    public class TermDetailViewModel : BaseViewModel
    {
        public string Title { get; set; }

        public TermDetailViewModel(Term term)
        {
            Title = term.Title;
        }
    }
}
