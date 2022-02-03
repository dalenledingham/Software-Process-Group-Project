using PatientManager.ViewModel;
using System.Windows;

namespace PatientManager
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new MainWindowViewModel();

            InitializeComponent();
        }
    }
}
