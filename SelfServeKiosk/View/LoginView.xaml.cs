using System;
using System.Windows;

namespace SelfServeKiosk.View
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        /*********************************************
         * DEVELOPMENT CODE - REMOVE BEFORE RELEASE
         *********************************************/
        private void DebugExitClick(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        /*********************************************
         * END DEVELOPMENT CODE
         *********************************************/
    }
}
