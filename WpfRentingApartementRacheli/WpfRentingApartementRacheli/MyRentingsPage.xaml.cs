using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfRentingApartementRacheli.ServiceReference1;

namespace WpfRentingApartementRacheli
{
    public partial class MyRentingsPage : Page
    {
        Service1Client service = new Service1Client();

        public MyRentingsPage()
        {
            InitializeComponent();

            if (Global.CurrentRole != Global.UserRole.Hirer || Global.CurrentHirer == null)
            {
                MessageBox.Show("יש להתחבר כשוכר", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                NavigationService?.Navigate(new pageLogin());
                return;
            }

            LoadRentings();
        }

        private void LoadRentings()
        {
            string hirerId = Global.CurrentHirer.C_IDHirer;
            dgRentings.ItemsSource = service.GetTORentings()
                .Where(r => r.IdHirer != null && r.IdHirer.C_IDHirer == hirerId)
                .OrderBy(r => r.Date)
                .ToList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new SearchW());
        }
    }
}
