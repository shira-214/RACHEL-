using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfRentingApartementRacheli.ServiceReference1;

namespace WpfRentingApartementRacheli
{
    public partial class AddManagerApartments : Page
    {
        Service1Client server = new Service1Client();
        DTOApartments Apartment;
        bool Add;

        public AddManagerApartments()
        {
            InitializeComponent();
            Apartment = new DTOApartments { Status = true };
            DataContext = Apartment;
            Add = true;
            ApartmentCities.ItemsSource = server.GetTOCities();
        }

        public AddManagerApartments(DTOApartments apartment)
        {
            InitializeComponent();
            Apartment = apartment;
            DataContext = Apartment;
            Add = false;
            ApartmentCities.ItemsSource = server.GetTOCities();

            if (Apartment.IdCities != null)
            {
                ApartmentCities.SelectedItem = server.GetTOCities()
                    .FirstOrDefault(c => c.IdCity == Apartment.IdCities.IdCity);
                LoadStreetsForCity(Apartment.IdCities.IdCity);
                if (Apartment.IdStreet != null && ApartmentStreetsNames.ItemsSource is System.Collections.IEnumerable streets)
                {
                    foreach (DTOStreetsNames street in streets)
                    {
                        if (street.IdStreet == Apartment.IdStreet.IdStreet)
                        {
                            ApartmentStreetsNames.SelectedItem = street;
                            break;
                        }
                    }
                }
            }
        }

        private void ApartmentCities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApartmentStreetsNames.ItemsSource = null;
            ApartmentStreetsNames.SelectedItem = null;

            if (ApartmentCities.SelectedItem is DTOCities city)
                LoadStreetsForCity(city.IdCity);
        }

        private void LoadStreetsForCity(int cityId)
        {
            ApartmentStreetsNames.ItemsSource = server.GetTOAraesCitiesStreets()
                .Where(x => x.IdCities != null && x.IdCities.IdCity == cityId && x.IdStreetDTo != null)
                .GroupBy(x => x.IdStreetDTo.IdStreet)
                .Select(g => g.First().IdStreetDTo)
                .ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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

            if (Add)
            {
                if (!server.AddApartments(Apartment))
                {
                    MessageBox.Show("שגיאה בהוספת הדירה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var saved = RefreshApartmentFromServer(Apartment);
                if (saved == null)
                {
                    MessageBox.Show("הדירה נוספה אך לא ניתן לטעון את המזהה", "אזהרה",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    NavigationService?.Navigate(new AllApartments());
                    return;
                }

                Apartment = saved;
                NavigationService?.Navigate(new POrders(Apartment));
            }
            else
            {
                if (server.UpdateApartments(Apartment))
                {
                    MessageBox.Show("עודכן בהצלחה!", "הצלחה", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService?.Navigate(new POrders(Apartment));
                }
                else
                    MessageBox.Show("שגיאה בעדכון", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private DTOApartments RefreshApartmentFromServer(DTOApartments apartment)
        {
            return server.GetApartments()
                .FirstOrDefault(a =>
                    a.NameOwner == apartment.NameOwner &&
                    a.PhoneOwner == apartment.PhoneOwner &&
                    a.NumberHouse == apartment.NumberHouse &&
                    a.IdCities != null && apartment.IdCities != null &&
                    a.IdCities.IdCity == apartment.IdCities.IdCity &&
                    a.IdStreet != null && apartment.IdStreet != null &&
                    a.IdStreet.IdStreet == apartment.IdStreet.IdStreet);
        }
    }
}
