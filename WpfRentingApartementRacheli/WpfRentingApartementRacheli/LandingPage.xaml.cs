using System.Windows;
using System.Windows.Controls;

namespace WpfRentingApartementRacheli
{
    public partial class LandingPage : Page
    {
        public LandingPage()
        {
            InitializeComponent();
        }

        private void btnHirer_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new pageLogin());
        }

        private void btnOwner_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OwnerLoginPage());
        }

        private void btnManager_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ManagerLoginPage());
        }
    }
}
