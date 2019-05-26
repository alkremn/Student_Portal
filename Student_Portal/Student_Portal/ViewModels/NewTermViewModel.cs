using Student_Portal.Models;
using Student_Portal.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Student_Portal.ViewModels
{
    class NewTermViewModel : BaseViewModel
    {
        private Term _term;
        private TermDataService _termData;
        private DateTime _startDate;
        private DateTime _endDate;
        public string Title { get; set; }

        public ICommand SaveCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public NewTermViewModel(TermDataService termData, Term term)
        {
            _term = term;
            _termData = termData;
            if (term != null)
            {
                Title = term.Title;
                _startDate = term.StartDate;
                _endDate = term.EndDate;
            }
            else
            {
                _startDate = DateTime.Today;
                _endDate = DateTime.Today;
            }

            SaveCommand = new Command(async () => await OnNewTermSave());
            CancelCommand = new Command(async () => await OnCancelClicked());
        }
       
        private async Task OnNewTermSave()
        {
            if(_term == null)
                _term = new Term();

            _term.Title = Title;
            _term.StartDate = StartDate;
            _term.EndDate = EndDate;

            await _termData.SaveTermAsync(_term);

            MessagingCenter.Send(this, App.saved);

            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async Task OnCancelClicked()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
