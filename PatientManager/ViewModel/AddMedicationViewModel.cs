using PatientManagerCommon.MVVM;
using System;

namespace PatientManager.ViewModel
{
    public class AddMedicationViewModel : ViewModelBase
    {
        private string medicationName, dosage;

        public bool Create { get; set; }
        public event EventHandler OnRequestClose;

        public RelayCommand CreateMedicationCommand => new RelayCommand(param => CreateMedication(), predicate => CanCreateMedication());
        public RelayCommand CancelCommand => new RelayCommand(param => Cancel());

        public string MedicationName
        {
            get => medicationName;
            set
            {
                if (medicationName == value) return;
                medicationName = value;
                OnPropertyChanged();
            }
        }

        public string Dosage
        {
            get => dosage;
            set
            {
                if (dosage == value) return;
                dosage = value;
                OnPropertyChanged();
            }
        }

        private void CreateMedication()
        {
            Create = true;
            OnRequestClose(this, new EventArgs());
        }

        private void Cancel()
        {
            OnRequestClose(this, new EventArgs());
        }

        private bool CanCreateMedication()
        {
            return !string.IsNullOrEmpty(MedicationName)
                && !string.IsNullOrEmpty(Dosage);
        }
    }
}
