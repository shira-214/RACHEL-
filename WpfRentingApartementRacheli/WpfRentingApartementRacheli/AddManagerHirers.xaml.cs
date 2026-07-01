using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfRentingApartementRacheli.ServiceReference1;

namespace WpfRentingApartementRacheli
{
    public partial class AddManagerHirers : Page
    {
        Service1Client server = new Service1Client();
        DTOHirers hirer;
        bool Add;

        public AddManagerHirers()
        {
            InitializeComponent();
            hirer = new DTOHirers();
            DataContext = hirer;
            Add = true;
        }

        public AddManagerHirers(DTOHirers hirers)
        {
            InitializeComponent();
            hirer = hirers;
            DataContext = hirer;
            Add = false;
        }

        private void btnEnd_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(txtExtras) || Validation.GetHasError(txtFhone))
            {
                MessageBox.Show("נתונים שגויים!", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (txtExtras.Text == "" || txtFhone.Text == "")
            {
                MessageBox.Show("נתונים חסרים!", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (Add)
            {
                if (server.GetTOHirers().FirstOrDefault(x => x.C_IDHirer == hirer.C_IDHirer) != null)
                {
                    MessageBox.Show("שוכר קיים", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!server.AddHirers(hirer))
                {
                    MessageBox.Show("שגיאה בהוספה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (Global.isManager == false)
                {
                    Global.currentHirers = server.GetTOHirers()
                        .FirstOrDefault(x => x.C_IDHirer == hirer.C_IDHirer);
                    NavigationService.Navigate(new SearchW());
                }
                else
                    NavigationService.Navigate(new AllHirers());
            }
            else
            {
                if (server.UpdateHirers(hirer))
                    MessageBox.Show("עודכן בהצלחה!", "הצלחה", MessageBoxButton.OK, MessageBoxImage.Information);
                else
                    MessageBox.Show("שגיאה בעדכון", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);

                NavigationService.Navigate(new AllHirers());
            }
        }
    }
}
