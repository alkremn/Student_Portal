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
        private bool isEndDateSelected = false;
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
                    IsStartDateValid = true;
                    IsEndDateValid = true;
                }
                else
                {
                    IsStartDateValid = false;
                    IsEndDateValid = false;
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
                isEndDateSelected = true;

                if (value.Date >= StartDateSelected.Date)
                {
                    IsEndDateValid = true;
                    IsStartDateValid = true;
                }
                else
                {
                    IsStartDateValid = false;
                    IsEndDateValid = false;
                }
            }
        }

        private bool isStartDateValid;
        public bool IsStartDateValid
        {
            get => isStartDateValid;
            set
            {
                isStartDateValid = value;
                OnPropertyChanged();
            }
        }

        private bool isEndDateValid;
        public bool IsEndDateValid
        {
            get => isEndDateValid;
            set
            {
                isEndDateValid = value;
                OnPropertyChanged();
            }
        }


        public NewTermViewModel(TermDataService termData, Term term)
        {
            _term = term;
            _termData = termData;
            if (term != null)
            {
                Title = term.Title;
                _startDateSelected = term.StartDate;
                _endDateSelected = term.EndDate;
            }
            else
            {
                _startDateSelected = DateTime.Today;
                _endDateSelected = DateTime.Today;
            }

            SaveCommand = new Command(OnNewTermSave, CanTermSave);
            CancelCommand = new Command(async () => await OnCancelClicked());
        }

        private bool CanTermSave()
        {
            return !string.IsNullOrWhiteSpace(Title) && isEndDateSelected  && isStartDateValid && IsEndDateValid;
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
