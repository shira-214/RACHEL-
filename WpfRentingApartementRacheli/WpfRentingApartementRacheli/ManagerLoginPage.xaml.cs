using System.Windows;
using System.Windows.Controls;

namespace WpfRentingApartementRacheli
{
    public partial class ManagerLoginPage : Page
    {
        public ManagerLoginPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtUser.Text == "" || txtPass.Password == "")
            {
                MessageBox.Show("יש למלא שם משתמש וסיסמה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (txtUser.Text == "admin" && txtPass.Password == "1234")
            {
                Global.CurrentRole = Global.UserRole.Manager;
                Global.isManager = true;
                NavigationService.Navigate(new ManagerPage());
            }
            else
            {
                MessageBox.Show("פרטי כניסה שגויים", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
            else
                NavigationService.Navigate(new LandingPage());
        }
    }
}
