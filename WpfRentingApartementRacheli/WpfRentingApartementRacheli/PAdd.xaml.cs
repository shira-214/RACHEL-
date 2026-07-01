using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfRentingApartementRacheli.ServiceReference1;

namespace WpfRentingApartementRacheli
{
    public partial class AddW : Page
    {
        const int OtherStreetId = -1;
        const int OtherExtraId = -1;

        Service1Client server = new Service1Client();
        DTOApartments Apartment;
        bool isEdit;
        List<string> selectedExtraNames = new List<string>();

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
            LoadExtrasCombo();

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
            LoadExtrasCombo();

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

        private void LoadExtrasCombo()
        {
            var extras = server.GetExtras().ToList();
            extras.Add(new DTOExtras { IdExtra = OtherExtraId, NameExtra = "אחר" });
            cmbExtras.ItemsSource = extras;
        }

        private void ApartmentCities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApartmentStreetsNames.ItemsSource = null;
            txtOtherStreet.Visibility = Visibility.Collapsed;
            txtOtherStreet.Text = "";

            if (!isEdit)
                ApartmentStreetsNames.SelectedItem = null;

            if (ApartmentCities.SelectedItem is DTOCities city)
                LoadStreetsForCity(city.IdCity);
        }

        private void LoadStreetsForCity(int cityId)
        {
            var streets = server.GetTOAraesCitiesStreets()
                .Where(x => x.IdCities != null && x.IdCities.IdCity == cityId && x.IdStreetDTo != null)
                .GroupBy(x => x.IdStreetDTo.IdStreet)
                .Select(g => g.First().IdStreetDTo)
                .ToList();
            streets.Add(new DTOStreetsNames { IdStreet = OtherStreetId, StreetName = "אחר" });
            ApartmentStreetsNames.ItemsSource = streets;
        }

        private void ApartmentStreetsNames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsOtherStreetSelected())
            {
                txtOtherStreet.Visibility = Visibility.Visible;
                txtOtherStreet.Text = "";
            }
            else
            {
                txtOtherStreet.Visibility = Visibility.Collapsed;
                txtOtherStreet.Text = "";
            }
        }

        private void cmbExtras_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbExtras.SelectedItem is DTOExtras extra && extra.IdExtra == OtherExtraId)
            {
                txtOtherExtra.Visibility = Visibility.Visible;
                txtOtherExtra.Text = "";
            }
            else
            {
                txtOtherExtra.Visibility = Visibility.Collapsed;
                txtOtherExtra.Text = "";
            }
        }

        private void btnAddExtra_Click(object sender, RoutedEventArgs e)
        {
            if (!(cmbExtras.SelectedItem is DTOExtras selected))
            {
                MessageBox.Show("יש לבחור תוספת", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string name;
            if (selected.IdExtra == OtherExtraId)
            {
                if (string.IsNullOrWhiteSpace(txtOtherExtra.Text))
                {
                    MessageBox.Show("יש להקליד שם תוספת", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                name = txtOtherExtra.Text.Trim();
            }
            else
                name = selected.NameExtra;

            if (selectedExtraNames.Contains(name))
            {
                MessageBox.Show("תוספת זו כבר נוספה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            selectedExtraNames.Add(name);
            lbSelectedExtras.ItemsSource = null;
            lbSelectedExtras.ItemsSource = selectedExtraNames.ToList();
            txtOtherExtra.Text = "";
            txtOtherExtra.Visibility = Visibility.Collapsed;
        }

        private bool IsOtherStreetSelected()
        {
            return ApartmentStreetsNames.SelectedItem is DTOStreetsNames street && street.IdStreet == OtherStreetId;
        }

        private DTOStreetsNames ResolveSelectedStreet()
        {
            if (!(ApartmentStreetsNames.SelectedItem is DTOStreetsNames street))
                return null;

            if (street.IdStreet != OtherStreetId)
                return street;

            if (string.IsNullOrWhiteSpace(txtOtherStreet.Text))
                return null;

            string name = txtOtherStreet.Text.Trim();
            if (!(ApartmentCities.SelectedItem is DTOCities city))
                return null;

            var streetRecord = server.GetStreetsNames().FirstOrDefault(s => s.StreetName == name);
            if (streetRecord == null)
            {
                if (!server.AddStreetsNames(new DTOStreetsNames { StreetName = name }))
                    return null;
                streetRecord = server.GetStreetsNames().FirstOrDefault(s => s.StreetName == name);
            }
            if (streetRecord == null)
                return null;

            bool linkedToCity = server.GetTOAraesCitiesStreets().Any(x =>
                x.IdCities != null && x.IdCities.IdCity == city.IdCity &&
                x.IdStreetDTo != null && x.IdStreetDTo.IdStreet == streetRecord.IdStreet);
            if (linkedToCity)
                return streetRecord;

            var cityLink = server.GetTOAraesCitiesStreets()
                .FirstOrDefault(x => x.IdCities != null && x.IdCities.IdCity == city.IdCity && x.IdArea != null);
            DTOAreas area = cityLink?.IdArea ?? server.GetAreas().FirstOrDefault();
            if (area == null)
                return null;

            if (!server.AddAraesCitiesStreet(new DTOAraesCitiesStreet
            {
                IdCities = city,
                IdStreetDTo = streetRecord,
                IdArea = area
            }))
                return null;

            return streetRecord;
        }

        private void SaveSelectedExtras()
        {
            if (Apartment.IdApartment <= 0 || selectedExtraNames.Count == 0)
                return;

            foreach (string name in selectedExtraNames)
            {
                DTOExtras extra = server.GetExtras().FirstOrDefault(x => x.NameExtra == name);
                if (extra == null)
                {
                    server.AddExtras(new DTOExtras { NameExtra = name });
                    extra = server.GetExtras().FirstOrDefault(x => x.NameExtra == name);
                }
                if (extra == null)
                    continue;

                bool exists = server.GetTOExtrasApartements().Any(x =>
                    x.IdExtra != null && x.IdAapartment != null &&
                    x.IdExtra.IdExtra == extra.IdExtra &&
                    x.IdAapartment.IdApartment == Apartment.IdApartment);
                if (exists)
                    continue;

                server.AddExtrasApartements(new DTOExtrasApartements
                {
                    IdExtra = extra,
                    IdAapartment = new DTOApartments { IdApartment = Apartment.IdApartment },
                    Status = true
                });
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

            if (IsOtherStreetSelected() && string.IsNullOrWhiteSpace(txtOtherStreet.Text))
            {
                MessageBox.Show("יש להקליד שם רחוב", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Apartment.IdCities = (DTOCities)ApartmentCities.SelectedItem;
            DTOStreetsNames street = ResolveSelectedStreet();
            if (street == null)
            {
                MessageBox.Show("שגיאה בשמירת הרחוב", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Apartment.IdStreet = street;

            if (isEdit)
            {
                Apartment.Status = chkStatus.IsChecked == true;
                if (server.UpdateApartments(Apartment))
                {
                    SaveSelectedExtras();
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

                SaveSelectedExtras();
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

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
            else
                NavigationService.Navigate(new LandingPage());
        }
    }
}
