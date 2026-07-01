using System.Windows;
using System.Windows.Controls;
using WpfRentingApartementRacheli.ServiceReference1;

namespace WpfRentingApartementRacheli
{
    public partial class AddManagerStreetNames : Page
    {
        Service1Client server = new Service1Client();
        DTOStreetsNames streetsNames;
        bool Add;

        public AddManagerStreetNames()
        {
            InitializeComponent();
            streetsNames = new DTOStreetsNames();
            DataContext = streetsNames;
            Add = true;
        }

        public AddManagerStreetNames(DTOStreetsNames streetsNames)
        {
            InitializeComponent();
            this.streetsNames = streetsNames;
            DataContext = streetsNames;
            Add = false;
        }

        private void btnEnd_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(txtStreet))
            {
                MessageBox.Show("נתונים שגויים!", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (txtStreet.Text == "")
            {
                MessageBox.Show("נתונים חסרים!", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool ok;
            if (Add)
                ok = server.AddStreetsNames(streetsNames);
            else
                ok = server.UpdateStreetsNames(streetsNames);

            if (ok)
            {
                MessageBox.Show(Add ? "נוסף בהצלחה!" : "עודכן בהצלחה!", "הצלחה",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService?.Navigate(new AllStreetsNames());
            }
            else
                MessageBox.Show("שגיאה בשמירה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
