using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfRentingApartementRacheli.ServiceReference1;

namespace WpfRentingApartementRacheli
{
    public partial class OwnerLoginPage : Page
    {
        Service1Client server = new Service1Client();

        public OwnerLoginPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtOwnerName.Text == "" || txtPhone.Text == "")
            {
                MessageBox.Show("יש למלא שם וטלפון", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                Global.currentApartments = server.GetApartments()
                    .FirstOrDefault(x => x.NameOwner == txtOwnerName.Text && x.PhoneOwner == txtPhone.Text);

                if (Global.currentApartments != null)
                {
                    Global.CurrentRole = Global.UserRole.Owner;
                    NavigationService.Navigate(new OwnerDashboardPage());
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show(
                        "משכיר לא נמצא. האם ברצונך להירשם?",
                        "משתמש לא נמצא",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        Global.isManager = false;
                        NavigationService.Navigate(new AddW());
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("לא ניתן להתחבר לשרת. ודאי ש-Host.exe רץ.\n" + ex.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Global.isManager = false;
            NavigationService.Navigate(new AddW());
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
