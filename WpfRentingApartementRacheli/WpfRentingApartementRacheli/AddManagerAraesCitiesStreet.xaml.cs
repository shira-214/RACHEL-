using System.Windows;
using System.Windows.Controls;
using WpfRentingApartementRacheli.ServiceReference1;

namespace WpfRentingApartementRacheli
{
    public partial class AddManagerAraesCitiesStreet : Page
    {
        Service1Client server = new Service1Client();
        DTOAraesCitiesStreet link;
        bool add;

        public AddManagerAraesCitiesStreet()
        {
            InitializeComponent();
            link = new DTOAraesCitiesStreet();
            add = true;
            LoadCombos();
        }

        public AddManagerAraesCitiesStreet(DTOAraesCitiesStreet existing)
        {
            InitializeComponent();
            link = existing;
            add = false;
            LoadCombos();
            ApartmentCities.SelectedItem = existing.IdCities;
            ApartmentStreetsNames.SelectedItem = existing.IdStreetDTo;
            ApartmentArea.SelectedItem = existing.IdArea;
        }

        private void LoadCombos()
        {
            ApartmentCities.ItemsSource = server.GetTOCities();
            ApartmentStreetsNames.ItemsSource = server.GetStreetsNames();
            ApartmentArea.ItemsSource = server.GetAreas();
        }

        private void btnEnd_Click(object sender, RoutedEventArgs e)
        {
            if (ApartmentCities.SelectedItem == null || ApartmentStreetsNames.SelectedItem == null || ApartmentArea.SelectedItem == null)
            {
                MessageBox.Show("יש לבחור עיר, רחוב ואזור", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            link.IdCities = (DTOCities)ApartmentCities.SelectedItem;
            link.IdStreetDTo = (DTOStreetsNames)ApartmentStreetsNames.SelectedItem;
            link.IdArea = (DTOAreas)ApartmentArea.SelectedItem;

            bool ok;
            if (add)
                ok = server.AddAraesCitiesStreet(link);
            else
                ok = server.UpdateraesCitiesStreet(link);

            if (ok)
            {
                MessageBox.Show(add ? "נוסף בהצלחה!" : "עודכן בהצלחה!", "הצלחה",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService?.Navigate(new AllAraesCitiesStreet());
            }
            else
                MessageBox.Show("שגיאה בשמירה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
