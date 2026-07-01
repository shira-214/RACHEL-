using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfRentingApartementRacheli.ServiceReference1;

namespace WpfRentingApartementRacheli
{
    public partial class AddW : Page
    {
        Service1Client server = new Service1Client();
        DTOApartments Apartment;

        public AddW()
        {
            InitializeComponent();

            if (Global.CurrentRole != Global.UserRole.Owner && Global.CurrentRole != Global.UserRole.None)
            {
                MessageBox.Show("יש להתחבר כמשכיר", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                NavigationService?.Navigate(new OwnerLoginPage());
                return;
            }

            Apartment = new DTOApartments();
            DataContext = Apartment;
            ApartmentCities.ItemsSource = server.GetTOCities();

            if (Global.CurrentRole == Global.UserRole.Owner && Global.CurrentOwner != null)
            {
                Apartment.NameOwner = Global.CurrentOwner.NameOwner;
                Apartment.PhoneOwner = Global.CurrentOwner.PhoneOwner;
                ApartmentsNameOwner.IsReadOnly = true;
                txtPhoneOwner.IsReadOnly = true;
            }
        }

        private void ApartmentCities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApartmentStreetsNames.ItemsSource = null;
            ApartmentStreetsNames.SelectedItem = null;

            if (ApartmentCities.SelectedItem is DTOCities city)
            {
                ApartmentStreetsNames.ItemsSource = server.GetTOAraesCitiesStreets()
                    .Where(x => x.IdCities != null && x.IdCities.IdCity == city.IdCity && x.IdStreetDTo != null)
                    .GroupBy(x => x.IdStreetDTo.IdStreet)
                    .Select(g => g.First().IdStreetDTo)
                    .ToList();
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(ApartmentsNameOwner) || Validation.GetHasError(txtPhoneOwner) ||
                Validation.GetHasError(txtNumberHouse) || Validation.GetHasError(txtFloor) ||
                Validation.GetHasError(txtNumberRooms) || Validation.GetHasError(txtNumberBeds) ||
                Validation.GetHasError(txtMinimumPrice) || Validation.GetHasError(txtExtraForBed))
            {
                MessageBox.Show("נתונים שגויים!", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (ApartmentsNameOwner.Text == "" || txtPhoneOwner.Text == "" ||
                ApartmentCities.SelectedItem == null || ApartmentStreetsNames.SelectedItem == null ||
                txtNumberHouse.Text == "" || txtFloor.Text == "" || txtNumberRooms.Text == "" ||
                txtNumberBeds.Text == "" || txtMinimumPrice.Text == "" || txtExtraForBed.Text == "" ||
                txtnote.Text == "")
            {
                MessageBox.Show("נתונים חסרים!", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Apartment.IdCities = (DTOCities)ApartmentCities.SelectedItem;
            Apartment.IdStreet = (DTOStreetsNames)ApartmentStreetsNames.SelectedItem;
            Apartment.Status = true;

            if (server.AddApartments(Apartment))
            {
                Global.currentApartments = Apartment;
                Global.CurrentRole = Global.UserRole.Owner;
                MessageBox.Show("הדירה נוספה בהצלחה!", "הצלחה", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService?.Navigate(new OwnerDashboardPage());
            }
            else
            {
                MessageBox.Show("שגיאה בהוספת הדירה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
