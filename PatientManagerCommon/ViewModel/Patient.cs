using PatientManagerCommon.MVVM;
using System.Collections.ObjectModel;

namespace PatientManagerCommon.ViewModel
{
    public class Patient : ViewModelBase
    {
        private string firstName;
        private string lastName;
        private string accountNumber;
        private ObservableCollection<Medication> medications = new ObservableCollection<Medication>();

        public int PatientID { get; set; }

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

        public ObservableCollection<Medication> Medications
        {
            get => medications;
            set
            {
                if (medications == value) return;
                medications = value;
                OnPropertyChanged();
            }
        }

        public override string ToString()
        {
            return LastName + ", " + FirstName;
        }
    }
}
