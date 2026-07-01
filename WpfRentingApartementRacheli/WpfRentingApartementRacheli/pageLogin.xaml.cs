using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfRentingApartementRacheli.ServiceReference1;

namespace WpfRentingApartementRacheli
{
    public partial class pageLogin : Page
    {
        Service1Client server = new Service1Client();

        public pageLogin()
        {
            InitializeComponent();
            this.DataContext = new DTOHirers();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(txtId))
            {
                MessageBox.Show("תעודת זהות לא תקינה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string id = txtId.Text?.Trim();
            if (string.IsNullOrEmpty(id))
            {
                MessageBox.Show("יש להזין תעודת זהות", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Global.isManager = false;
            Global.CurrentHirer = server.GetTOHirers().FirstOrDefault(x => x.C_IDHirer == id);

            if (Global.CurrentHirer != null)
            {
                Global.CurrentRole = Global.UserRole.Hirer;
                NavigationService.Navigate(new SearchW());
            }
            else
            {
                MessageBoxResult result = MessageBox.Show(
                    "אינך רשום במערכת, האם ברצונך להירשם כעת?",
                    "משתמש לא נמצא",
                    MessageBoxButton.OKCancel,
                    MessageBoxImage.Information);

                if (result == MessageBoxResult.OK)
                {
                    NavigationService.Navigate(new AddManagerHirers());
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Global.isManager = false;
            NavigationService.Navigate(new AddManagerHirers());
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
