using System.Windows;
using System.Windows.Controls;
using WpfRentingApartementRacheli.ServiceReference1;

namespace WpfRentingApartementRacheli
{
    public partial class AllRentings : Page
    {
        Service1Client server = new Service1Client();

        public AllRentings()
        {
            InitializeComponent();
            lvRentings.ItemsSource = server.GetTORentings();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddManagerRentings());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (lvRentings.SelectedItem is DTORentings renting)
                NavigationService?.Navigate(new AddManagerRentings(renting));
            else
                MessageBox.Show("יש לבחור הזמנה לעריכה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (!(lvRentings.SelectedItem is DTORentings renting))
            {
                MessageBox.Show("יש לבחור הזמנה למחיקה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool ok = server.DeleteRenting(renting.IdRenting);
            if (ok)
            {
                MessageBox.Show("נמחק בהצלחה!", "הצלחה", MessageBoxButton.OK, MessageBoxImage.Information);
                lvRentings.ItemsSource = server.GetTORentings();
            }
            else
                MessageBox.Show("לא ניתן למחוק — קיימות רשומות מקושרות. מחק קודם את התלויות.", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
