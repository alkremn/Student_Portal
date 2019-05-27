using Student_Portal.Models;
using Student_Portal.Views;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Student_Portal.ViewModels
{
    public class NewCoursePage2ViewModel : BaseViewModel
    {
        private const int MAX_PHONE_NUMBER = 10;
        private Course _course;
        private Term _term;

        private string _insName;
        public string InsName
        {
            get => _insName;
            set
            {
                _insName = value;
                OnPropertyChanged();
                NextCommand.ChangeCanExecute();
            }
        }

        private string _phone;
        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged();
                NextCommand.ChangeCanExecute();
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
                NextCommand.ChangeCanExecute();
            }
        }

        public Command PrevCommand { get; }
        public Command NextCommand { get; }

        public NewCoursePage2ViewModel(Course selectedCourse, Term term)
        {
            _course = selectedCourse;
            _term = term;

            if (selectedCourse.IsExisting)
                InitCourseData(_course);

            PrevCommand = new Command(OnPrevClicked);
            NextCommand = new Command(OnNextClicked, CanCextClicked);
        }

        private void InitCourseData(Course selectedCourse)
        {
            _insName = selectedCourse.InstructorName;
            _phone = selectedCourse.InstructorPhone;
            _email = selectedCourse.InstructorEmail;
        }

        private bool CanCextClicked(object arg)
        {
            return !string.IsNullOrWhiteSpace(InsName)
                && long.TryParse(_phone, out long _)
                && _phone.Length <= MAX_PHONE_NUMBER
                && ValidateEmailString(_email);
        }

        //checking if the string is a valid email address
        private bool ValidateEmailString(string emailString)
        {
            if (string.IsNullOrWhiteSpace(emailString))
                return false;

            try
            {
                MailAddress email = new MailAddress(emailString);
            }
            catch (FormatException)
            {
                return false;
            }
            return true;
        }

        private async void OnNextClicked(object obj)
        {
            _course.InstructorName = InsName;
            _course.InstructorPhone = Phone;
            _course.InstructorEmail = Email;
            await App.Current.MainPage.Navigation.PushAsync(new NewCoursePage3(_course, _term));
        }

        private async void OnPrevClicked(object obj)
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

    }
}
