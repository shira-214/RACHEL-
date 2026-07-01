using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfRentingApartementRacheli.ServiceReference1;
using WpfRentingApartementRacheli.USC;



namespace WpfRentingApartementRacheli
{
    /// <summary>
    /// Interaction logic for SearchW.xaml
    /// </summary>
    public partial class SearchW : Page
    {
        // יצירת מופע של השרת
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

            ApartmentCities.ItemsSource = service.GetTOCities();
            ApartmentStreetsNames.ItemsSource = service.GetStreetsNames();
            RefreshData();
        }

        // האירוע שקישרנו ב-XAML לכל הפקדים
        private void Filter_Changed(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }

        private void RefreshData()
        {
            lApartments = service.GetApartments().ToList();
            lRentings = service.GetTORentings().ToList();

            IEnumerable<DTOApartments> filtered = lApartments.Where(a => a.Status);

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
            foreach (DTOApartments apartment in filtered)
            {
                wrp.Children.Add(new USCApartement(apartment));
            }
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dp.SelectedDate.HasValue)
                Global.selectedDate = dp.SelectedDate.Value;

            RefreshData();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }
    }
}