using System.Windows;
using System.Windows.Controls;
using WpfRentingApartementRacheli.ServiceReference1;

namespace WpfRentingApartementRacheli
{
    public partial class AllStreetsNames : Page
    {
        Service1Client server = new Service1Client();

        public AllStreetsNames()
        {
            InitializeComponent();
            lvStreetNames.ItemsSource = server.GetStreetsNames();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddManagerStreetNames());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (lvStreetNames.SelectedItem is DTOStreetsNames street)
                NavigationService?.Navigate(new AddManagerStreetNames(street));
            else
                MessageBox.Show("יש לבחור רחוב לעדכון", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (!(lvStreetNames.SelectedItem is DTOStreetsNames street))
            {
                MessageBox.Show("יש לבחור רחוב למחיקה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool ok = server.DeleteStreetsName(street.IdStreet);
            if (ok)
            {
                MessageBox.Show("נמחק בהצלחה!", "הצלחה", MessageBoxButton.OK, MessageBoxImage.Information);
                lvStreetNames.ItemsSource = server.GetStreetsNames();
            }
            else
                MessageBox.Show("לא ניתן למחוק — קיימות רשומות מקושרות. מחק קודם את התלויות.", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
