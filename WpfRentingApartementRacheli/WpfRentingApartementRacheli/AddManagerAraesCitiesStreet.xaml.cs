using System.Linq;
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

            if (add)
            {
                var exists = server.GetTOAraesCitiesStreets().FirstOrDefault(x =>
                    x.IdStreetDTo != null && x.IdCities != null &&
                    x.IdStreetDTo.IdStreet == link.IdStreetDTo.IdStreet &&
                    x.IdCities.IdCity == link.IdCities.IdCity);

                if (exists != null)
                {
                    string areaName = exists.IdArea?.NameArea ?? "";
                    MessageBox.Show(
                        "קישור עיר-רחוב זה כבר קיים" +
                        (areaName != "" ? $" (אזור: {areaName})" : "") + ".\n" +
                        "לכל עיר ורחוב ניתן לשייך רשומה אחת בלבד.",
                        "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }

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
