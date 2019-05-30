using Student_Portal.Models;
using Student_Portal.Services;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Student_Portal.ViewModels
{
    class NewTermViewModel : BaseViewModel
    {
        private Term _term;
        private TermDataService _termData;

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();
            }
        }

        private DateTime _startDateSelected;
        public DateTime StartDateSelected
        {
            get => _startDateSelected;
            set
            {
                _startDateSelected = value;
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();

                if (value.Date <= EndDateSelected.Date)
                {
                    IsStartEndDateValid = true;
                }
                else
                {
                    IsStartEndDateValid = false;
                }
            }
        }

        private DateTime _endDateSelected;
        public DateTime EndDateSelected
        {
            get => _endDateSelected;
            set
            {
                _endDateSelected = value;
                OnPropertyChanged();
                SaveCommand.ChangeCanExecute();

                if (value.Date >= StartDateSelected.Date)
                {
                    IsStartEndDateValid = true;
                }
                else
                {
                    IsStartEndDateValid = false;
                }
            }
        }

        private bool isStartEndDateValid;
        public bool IsStartEndDateValid
        {
            get => isStartEndDateValid;
            set
            {
                isStartEndDateValid = value;
                OnPropertyChanged();
            }
        }

        public NewTermViewModel(TermDataService termData, Term term)
        {
            _term = term;
            _termData = termData;
            if (term != null)
            {
                _title = term.Title;
                _startDateSelected = term.StartDate;
                _endDateSelected = term.EndDate;
            }
            else
            {
                _startDateSelected = DateTime.Today;
                _endDateSelected = DateTime.Today;
            }
            isStartEndDateValid = true;

            SaveCommand = new Command(OnNewTermSave, CanTermSave);
            CancelCommand = new Command(async () => await OnCancelClicked());
        }

        private bool CanTermSave()
        {
            bool StartEndValid = _startDateSelected <= _endDateSelected;
            return !string.IsNullOrWhiteSpace(Title) && StartEndValid;
        }

        private async void OnNewTermSave()
        {
            if(_term == null)
                _term = new Term();

            _term.Title = Title;
            _term.StartDate = _startDateSelected;
            _term.EndDate = _endDateSelected;

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
