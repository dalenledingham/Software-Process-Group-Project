using PatientManagerCommon.MVVM;
using System;

namespace PatientManager.ViewModel
{
    public class AddPatientViewModel : ViewModelBase
    {
        private string firstName, lastName, accountNumber;

        public bool Create { get; set; }
        public event EventHandler OnRequestClose;

        public RelayCommand CreatePatientCommand => new RelayCommand(param => CreatePatient(), predicate => CanCreatePatient());
        public RelayCommand CancelCommand => new RelayCommand(param => Cancel());

        public string FirstName
        {
            get => firstName;
            set
            {
                if (firstName == value) return;
                firstName = value;
                OnPropertyChanged();
            }
        }

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

        private void CreatePatient()
        {
            Create = true;
            OnRequestClose(this, new EventArgs());
        }

        private void Cancel()
        {
            OnRequestClose(this, new EventArgs());
        }

        private bool CanCreatePatient()
        {
            return !string.IsNullOrEmpty(FirstName)
                && !string.IsNullOrEmpty(LastName)
                && !string.IsNullOrEmpty(AccountNumber);
        }
    }
}
