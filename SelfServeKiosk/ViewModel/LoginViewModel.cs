using PatientManagerCommon.Database;
using PatientManagerCommon.MVVM;
using PatientManagerCommon.ViewModel;
using System;
using System.Windows;

namespace SelfServeKiosk.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private string lastName, accountNumber;

        public Patient LoggedInPatient { get; private set; }
        public event EventHandler OnRequestClose;

        public RelayCommand LoginCommand => new RelayCommand(param => Login(), predicate => CanLogin());
        public RelayCommand CancelCommand => new RelayCommand(param => Cancel());

        public string LastName
        {
            get => lastName;
            set
            {
                if (lastName == value) return;
                lastName = value;
                OnPropertyChanged();
            }
        }

        public string AccountNumber
        {
            get => accountNumber;
            set
            {
                if (accountNumber == value) return;
                accountNumber = value;
                OnPropertyChanged();
            }
        }

        private void Login()
        {
            LoadPatientData(LastName, AccountNumber);

            if(LoggedInPatient != null)
                OnRequestClose(this, new EventArgs());
            else
            {

            }
        }

        private void LoadPatientData(string lastName, string accountNumber)
        {
            LoggedInPatient = DatabaseManager.GetPatient(lastName, accountNumber);

            if(LoggedInPatient == null)
                MessageBox.Show("No Patient Found.");
        }

        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(LastName)
                && !string.IsNullOrEmpty(AccountNumber);
        }

        private void Cancel()
        {
            OnRequestClose(this, new EventArgs());
        }
    }
}
