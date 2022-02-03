using PatientManagerCommon.ViewModel;
using PatientManagerCommon.Database;
using PatientManagerCommon.MVVM;
using System.Collections.ObjectModel;
using PatientManager.View;
using System.Windows;

namespace PatientManager.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<Patient> patients;
        private Patient selectedPatient;
        private Medication selectedMedication;

        public MainWindowViewModel()
        {
            DatabaseManager.InitializeDatabase();

            patients = new ObservableCollection<Patient>(DatabaseManager.GetAllPatients());
        }

        public RelayCommand AddPatientCommand => new RelayCommand(param => AddPatient());
        public RelayCommand DeletePatientCommand => new RelayCommand(param => DeletePatient(), predicate => HasPatientSelected());
        public RelayCommand AddMedicationCommand => new RelayCommand(param => AddMedication(), predicate => HasPatientSelected());
        public RelayCommand DeleteMedicationCommand => new RelayCommand(param => DeleteMedication());
 
        private void AddPatient()
        {
            AddPatientViewModel addPatientVM = new AddPatientViewModel();
            AddPatientView addPatientView = new AddPatientView
            {
                DataContext = addPatientVM,
                Owner = Application.Current.MainWindow
            };
            addPatientVM.OnRequestClose += (s, e) => addPatientView.Close();
            addPatientView.ShowDialog();

            if(addPatientVM.Create)
            {
                Patient patient = new Patient
                {
                    FirstName = addPatientVM.FirstName,
                    LastName = addPatientVM.LastName,
                    AccountNumber = addPatientVM.AccountNumber
                };
                Patients.Add(patient);

                patient.SaveInDatabase();
            }
        }

        private bool HasPatientSelected()
        {
            return SelectedPatient != null;
        }

        private void DeletePatient()
        {
            SelectedPatient.RemoveFromDatabase();
            Patients.Remove(SelectedPatient);
        }

        private void AddMedication()
        {
            AddMedicationViewModel addMedicationVM = new AddMedicationViewModel();
            AddMedicationView addMedicationView = new AddMedicationView
            {
                DataContext = addMedicationVM,
                Owner = Application.Current.MainWindow
            };
            addMedicationVM.OnRequestClose += (s, e) => addMedicationView.Close();
            addMedicationView.ShowDialog();

            if (addMedicationVM.Create)
            {
                Medication medication = new Medication
                {
                    PatientID = SelectedPatient.PatientID,
                    Name = addMedicationVM.MedicationName,
                    Dosage = addMedicationVM.Dosage
                };
                SelectedPatient.Medications.Add(medication);

                medication.SaveInDatabase();
            }
        }

        private void DeleteMedication()
        {
            SelectedMedication.RemoveFromDatabase();
            SelectedPatient.Medications.Remove(SelectedMedication);
        }

        public ObservableCollection<Patient> Patients
        {
            get => patients;
            set
            {
                if (patients == value) return;
                patients = value;
                OnPropertyChanged();
            }
        }

        public Patient SelectedPatient
        {
            get => selectedPatient;
            set
            {
                if (selectedPatient == value) return;
                selectedPatient = value;
                OnPropertyChanged();
            }
        }

        public Medication SelectedMedication
        {
            get => selectedMedication;
            set
            {
                if (selectedMedication == value) return;
                selectedMedication = value;
                OnPropertyChanged();
            }
        }
    }
}
