using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Student_Portal.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {


        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy; 
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //Method that fires property changed event
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
