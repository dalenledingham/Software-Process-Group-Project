using PatientManagerCommon.MVVM;
using System;

namespace PatientManagerCommon.ViewModel
{
    public class Medication : ViewModelBase
    {
        private string name;
        private string dosage;

        public int MedicationID { get; set; }
        public int PatientID { get; set; }

        public string Name
        {
            get => name;
            set
            {
                if (name == value) return;
                name = value;
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
    }
}
