using System.Windows;
using System.Windows.Controls;
using WpfRentingApartementRacheli.ServiceReference1;

namespace WpfRentingApartementRacheli
{
    public partial class AddManagerCities : Page
    {
        Service1Client server = new Service1Client();
        DTOCities city;
        bool add;

        public AddManagerCities()
        {
            InitializeComponent();
            city = new DTOCities();
            DataContext = city;
            add = true;
        }

        public AddManagerCities(DTOCities selectedCity)
        {
            InitializeComponent();
            city = selectedCity;
            DataContext = city;
            add = false;
        }

        public void btnEnd_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(txtNameCity))
            {
                MessageBox.Show("נתונים שגויים!", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (txtNameCity.Text == "")
            {
                MessageBox.Show("נתונים חסרים!", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool ok;
            if (add)
                ok = server.AddCities(city);
            else
                ok = server.UpdateCities(city);

            if (ok)
            {
                MessageBox.Show(add ? "נוסף בהצלחה!" : "עודכן בהצלחה!", "הצלחה",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService?.Navigate(new AllCities());
            }
            else
                MessageBox.Show("שגיאה בשמירה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
