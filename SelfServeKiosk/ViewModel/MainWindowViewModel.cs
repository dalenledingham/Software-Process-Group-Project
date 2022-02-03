using PatientManagerCommon.Database;
using PatientManagerCommon.MVVM;
using PatientManagerCommon.ViewModel;
using SelfServeKiosk.View;

namespace SelfServeKiosk.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string welcomeMessage;
        private Patient loggedInPatient;

        public MainWindowViewModel()
        {
            DatabaseManager.InitializeDatabase();

            ShowLogin();
        }

        public RelayCommand LogoutCommand => new RelayCommand(param => Logout());

        private void ShowLogin()
        {
            LoginViewModel loginVM = new LoginViewModel();
            while (loginVM.LoggedInPatient == null)
            {
                LoginView loginView = new LoginView
                {
                    DataContext = loginVM,
                };
                loginVM.OnRequestClose += (s, e) => loginView.Close();
                loginView.ShowDialog();
            }

            LoggedInPatient = loginVM.LoggedInPatient;
            WelcomeMessage = $"Welcome, {LoggedInPatient.FirstName}!";
        }

        private void Logout()
        {
            ShowLogin();
        }

        public string WelcomeMessage
        {
            get => welcomeMessage;
            set
            {
                if (welcomeMessage == value) return;
                welcomeMessage = value;
                OnPropertyChanged();
            }
        }

        public Patient LoggedInPatient
        {
            get => loggedInPatient;
            set
            {
                if (loggedInPatient == value) return;
                loggedInPatient = value;
                OnPropertyChanged();
            }
        }
    }
}
