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
        bool isEdit;

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

        public AddW(DTOApartments existing)
        {
            InitializeComponent();
            isEdit = true;

            if (Global.CurrentRole != Global.UserRole.Owner || Global.CurrentOwner == null)
            {
                MessageBox.Show("יש להתחבר כמשכיר", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                NavigationService?.Navigate(new OwnerLoginPage());
                return;
            }

            if (existing.NameOwner != Global.CurrentOwner.NameOwner ||
                existing.PhoneOwner != Global.CurrentOwner.PhoneOwner)
            {
                MessageBox.Show("אין הרשאה לערוך דירה זו", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                NavigationService?.Navigate(new OwnerDashboardPage());
                return;
            }

            Apartment = existing;
            DataContext = Apartment;
            Title = "עריכת דירה";
            btnSave.Content = "עדכן דירה";
            chkStatus.Visibility = Visibility.Visible;
            chkStatus.IsChecked = Apartment.Status;

            ApartmentsNameOwner.IsReadOnly = true;
            txtPhoneOwner.IsReadOnly = true;
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
            if (!isEdit)
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

            if (isEdit)
            {
                Apartment.Status = chkStatus.IsChecked == true;
                if (server.UpdateApartments(Apartment))
                {
                    MessageBox.Show("הדירה עודכנה בהצלחה!", "הצלחה", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService?.Navigate(new OwnerDashboardPage());
                }
                else
                    MessageBox.Show("שגיאה בעדכון הדירה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Apartment.Status = true;

            if (server.AddApartments(Apartment))
            {
                var saved = RefreshApartmentFromServer(Apartment);
                if (saved != null)
                    Apartment = saved;

                Global.currentApartments = Apartment;
                Global.CurrentRole = Global.UserRole.Owner;
                MessageBox.Show("הדירה נוספה בהצלחה!", "הצלחה", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService?.Navigate(new OwnerDashboardPage());
            }
            else
                MessageBox.Show("שגיאה בהוספת הדירה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
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
