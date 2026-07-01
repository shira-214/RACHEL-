using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfRentingApartementRacheli.ServiceReference1;

namespace WpfRentingApartementRacheli
{
    public partial class OwnerDashboardPage : Page
    {
        Service1Client server = new Service1Client();

        public OwnerDashboardPage()
        {
            InitializeComponent();

            if (Global.CurrentRole != Global.UserRole.Owner || Global.CurrentOwner == null)
            {
                MessageBox.Show("יש להתחבר כמשכיר", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                NavigationService?.Navigate(new LandingPage());
                return;
            }

            txtWelcome.Text = "שלום, " + Global.CurrentOwner.NameOwner;
            LoadData();
        }

        private void LoadData()
        {
            var apartments = server.GetApartments()
                .Where(a => a.NameOwner == Global.CurrentOwner.NameOwner
                         && a.PhoneOwner == Global.CurrentOwner.PhoneOwner)
                .ToList();

            lvApartments.ItemsSource = apartments;

            var apartmentIds = new HashSet<int>(apartments.Select(a => a.IdApartment));
            dgRentings.ItemsSource = server.GetTORentings()
                .Where(r => r.KodHapartment != null && apartmentIds.Contains(r.KodHapartment.IdApartment))
                .OrderBy(r => r.Date)
                .ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddW());
        }

        private void btnAddImage_Click(object sender, RoutedEventArgs e)
        {
            if (lvApartments.SelectedItem is DTOApartments apartment)
                NavigationService?.Navigate(new POrders(apartment));
            else
                MessageBox.Show("יש לבחור דירה מהרשימה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Global.Logout();
            NavigationService?.Navigate(new LandingPage());
        }
    }
}
