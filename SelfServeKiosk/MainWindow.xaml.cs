using SelfServeKiosk.ViewModel;
using System.Windows;

namespace SelfServeKiosk
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
