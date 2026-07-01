using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfRentingApartementRacheli.ServiceReference1;

namespace WpfRentingApartementRacheli
{
    public partial class AddManagerExtrasApartements : Page
    {
        Service1Client server = new Service1Client();
        DTOExtrasApartements link;
        bool add;

        public AddManagerExtrasApartements()
        {
            InitializeComponent();
            link = new DTOExtrasApartements { Status = true };
            DataContext = link;
            add = true;
            LoadCombos();
        }

        public AddManagerExtrasApartements(DTOExtrasApartements existing)
        {
            InitializeComponent();
            link = existing;
            DataContext = link;
            add = false;
            LoadCombos();

            if (link.IdAapartment != null)
                cmbApartment.SelectedItem = server.GetApartments()
                    .FirstOrDefault(a => a.IdApartment == link.IdAapartment.IdApartment);
            if (link.IdExtra != null)
                cmbExtra.SelectedItem = server.GetExtras()
                    .FirstOrDefault(x => x.IdExtra == link.IdExtra.IdExtra);

            cmbApartment.IsEnabled = false;
            cmbExtra.IsEnabled = false;
        }

        private void LoadCombos()
        {
            cmbApartment.ItemsSource = server.GetApartments();
            cmbExtra.ItemsSource = server.GetExtras();
        }

        private void btnEnd_Click(object sender, RoutedEventArgs e)
        {
            if (add)
            {
                if (cmbApartment.SelectedItem == null || cmbExtra.SelectedItem == null)
                {
                    MessageBox.Show("יש לבחור דירה ותוספת", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                link.IdAapartment = (DTOApartments)cmbApartment.SelectedItem;
                link.IdExtra = (DTOExtras)cmbExtra.SelectedItem;

                bool exists = server.GetTOExtrasApartements().Any(x =>
                    x.IdExtra != null && x.IdAapartment != null &&
                    x.IdExtra.IdExtra == link.IdExtra.IdExtra &&
                    x.IdAapartment.IdApartment == link.IdAapartment.IdApartment);

                if (exists)
                {
                    MessageBox.Show("קישור זה כבר קיים", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (server.AddExtrasApartements(link))
                {
                    MessageBox.Show("נוסף בהצלחה!", "הצלחה", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService?.Navigate(new AllExtrasApartements());
                }
                else
                    MessageBox.Show("שגיאה בהוספה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (server.UpdateExtrasApartements(link))
                {
                    MessageBox.Show("עודכן בהצלחה!", "הצלחה", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService?.Navigate(new AllExtrasApartements());
                }
                else
                    MessageBox.Show("שגיאה בעדכון", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
