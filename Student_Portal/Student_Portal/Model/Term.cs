using SQLite;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Student_Portal.Model
{
    [Table("Terms")]
    public class Term : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        private int _termId { get; set; }
        private string _title { get; set; }
        private DateTime _startDate { get; set; }
        private DateTime _endDate { get; set; }

        public int TermId
        {
            get => _termId;
            set
            {
                _termId = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged();
            }
        }

        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
