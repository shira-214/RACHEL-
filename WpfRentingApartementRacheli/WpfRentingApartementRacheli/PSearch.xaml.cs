using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfRentingApartementRacheli.ServiceReference1;
using WpfRentingApartementRacheli.USC;

namespace WpfRentingApartementRacheli
{
    public partial class SearchW : Page
    {
        Service1Client service = new Service1Client();
        List<DTOApartments> lApartments;
        List<DTORentings> lRentings;

        public SearchW()
        {
            InitializeComponent();

            if (Global.CurrentRole != Global.UserRole.Hirer)
            {
                MessageBox.Show("יש להתחבר כשוכר", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                NavigationService?.Navigate(new pageLogin());
                return;
            }

            ApartmentAreas.ItemsSource = service.GetAreas();
            ApartmentCities.ItemsSource = service.GetTOCities();
            ApartmentStreetsNames.ItemsSource = service.GetStreetsNames();
            ApartmentExtras.ItemsSource = service.GetExtras();
            Loaded += SearchW_Loaded;
        }

        private void SearchW_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }

        private void Filter_Changed(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;

            RefreshData();
        }

        private void RefreshData()
        {
            if (slPrice == null || slBeds == null || wrp == null)
                return;

            lApartments = service.GetApartments().ToList();
            lRentings = service.GetTORentings().ToList();

            IEnumerable<DTOApartments> filtered = lApartments.Where(a => a.Status);

            if (ApartmentAreas.SelectedItem is DTOAreas selectedArea)
            {
                var cityIds = service.GetTOAraesCitiesStreets()
                    .Where(x => x.IdArea != null && x.IdArea.IdArea == selectedArea.IdArea && x.IdCities != null)
                    .Select(x => x.IdCities.IdCity)
                    .Distinct()
                    .ToList();

                filtered = filtered.Where(a => a.IdCities != null && cityIds.Contains(a.IdCities.IdCity));
            }

            if (ApartmentCities.SelectedItem is DTOCities selectedCity)
            {
                filtered = filtered.Where(a =>
                    a.IdCities != null && a.IdCities.IdCity == selectedCity.IdCity);
            }

            if (ApartmentStreetsNames.SelectedItem is DTOStreetsNames selectedStreet)
            {
                filtered = filtered.Where(a =>
                    a.IdStreet != null && a.IdStreet.IdStreet == selectedStreet.IdStreet);
            }

            int maxPrice = (int)slPrice.Value;
            filtered = filtered.Where(a => a.MinimumPrice <= maxPrice);

            int minBeds = (int)slBeds.Value;
            filtered = filtered.Where(a => a.NumberBeds >= minBeds);

            if (ApartmentExtras.SelectedItem is DTOExtras selectedExtra)
            {
                var apartmentIds = service.GetTOExtrasApartements()
                    .Where(x => x.IdExtra != null && x.IdExtra.IdExtra == selectedExtra.IdExtra &&
                                x.Status && x.IdAapartment != null)
                    .Select(x => x.IdAapartment.IdApartment)
                    .ToList();

                filtered = filtered.Where(a => apartmentIds.Contains(a.IdApartment));
            }

            if (dp.SelectedDate.HasValue)
            {
                DateTime searchDate = dp.SelectedDate.Value.Date;
                var bookedApartmentIds = lRentings
                    .Where(r => r.KodHapartment != null && r.Date.Date == searchDate)
                    .Select(r => r.KodHapartment.IdApartment)
                    .ToList();

                filtered = filtered.Where(a => !bookedApartmentIds.Contains(a.IdApartment));
            }

            wrp.Children.Clear();
            var allImages = service.GetImages();
            foreach (DTOApartments apartment in filtered)
                wrp.Children.Add(new USCApartement(apartment, allImages));
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded)
                return;

            if (dp.SelectedDate.HasValue)
                Global.selectedDate = dp.SelectedDate.Value;

            RefreshData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }

        private void btnClearFilters_Click(object sender, RoutedEventArgs e)
        {
            ApartmentAreas.SelectedItem = null;
            ApartmentCities.SelectedItem = null;
            ApartmentStreetsNames.SelectedItem = null;
            ApartmentExtras.SelectedItem = null;
            dp.SelectedDate = null;
            slPrice.Value = 5000;
            slBeds.Value = 1;
            Global.selectedDate = default(DateTime);
            RefreshData();
        }
    }
}
