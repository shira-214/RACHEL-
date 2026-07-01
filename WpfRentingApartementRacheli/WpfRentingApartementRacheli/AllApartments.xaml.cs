using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfRentingApartementRacheli.ServiceReference1;

namespace WpfRentingApartementRacheli
{
    public partial class AllApartments : Page
    {
        Service1Client server = new Service1Client();
        List<DTOApartments> allApartments;

        public AllApartments()
        {
            InitializeComponent();
            allApartments = server.GetApartments().ToList();
            cmbCity.ItemsSource = server.GetTOCities();
            cmbStreet.ItemsSource = server.GetStreetsNames();
            Loaded += AllApartments_Loaded;
        }

        private void AllApartments_Loaded(object sender, RoutedEventArgs e)
        {
            ApplyFilters();
        }

        private void Filter_Changed(object sender, RoutedEventArgs e)
        {
            if (!IsLoaded)
                return;

            ApplyFilters();
        }

        private void ApplyFilters()
        {
            if (slPrice == null || slBeds == null || lvApartment == null)
                return;

            IEnumerable<DTOApartments> filtered = allApartments;

            if (cmbCity.SelectedItem is DTOCities city)
            {
                filtered = filtered.Where(a =>
                    a.IdCities != null && a.IdCities.IdCity == city.IdCity);
            }

            if (cmbStreet.SelectedItem is DTOStreetsNames street)
            {
                filtered = filtered.Where(a =>
                    a.IdStreet != null && a.IdStreet.IdStreet == street.IdStreet);
            }

            int minBeds = (int)slBeds.Value;
            filtered = filtered.Where(a => a.NumberBeds >= minBeds);

            int maxPrice = (int)slPrice.Value;
            filtered = filtered.Where(a => a.MinimumPrice <= maxPrice);

            if (cmbStatus.SelectedItem is ComboBoxItem statusItem)
            {
                string statusText = statusItem.Content?.ToString();
                if (statusText == "פעיל")
                    filtered = filtered.Where(a => a.Status);
                else if (statusText == "לא פעיל")
                    filtered = filtered.Where(a => !a.Status);
            }

            lvApartment.ItemsSource = filtered.ToList();
        }

        private void btnClearFilters_Click(object sender, RoutedEventArgs e)
        {
            cmbCity.SelectedItem = null;
            cmbStreet.SelectedItem = null;
            slBeds.Value = 1;
            slPrice.Value = 5000;
            cmbStatus.SelectedIndex = 0;
            ApplyFilters();
        }

        private void ReloadApartments()
        {
            allApartments = server.GetApartments().ToList();
            ApplyFilters();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddManagerApartments());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (lvApartment.SelectedItem != null)
                NavigationService?.Navigate(new AddManagerApartments(lvApartment.SelectedItem as DTOApartments));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (!(lvApartment.SelectedItem is DTOApartments apt))
            {
                MessageBox.Show("יש לבחור דירה למחיקה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool ok = server.DeleteApartment(apt.IdApartment);
            if (ok)
            {
                MessageBox.Show("נמחק בהצלחה!", "הצלחה", MessageBoxButton.OK, MessageBoxImage.Information);
                ReloadApartments();
            }
            else
                MessageBox.Show("לא ניתן למחוק — קיימות רשומות מקושרות (הזמנות/תמונות). מחק קודם את התלויות.", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (!(lvApartment.SelectedItem is DTOApartments apt))
            {
                MessageBox.Show("יש לבחור דירה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NavigationService?.Navigate(new POrders(apt));
        }
    }
}
