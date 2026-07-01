using System.Windows;
using System.Windows.Controls;
using WpfRentingApartementRacheli.ServiceReference1;

namespace WpfRentingApartementRacheli
{
    public partial class AllExtrasApartements : Page
    {
        Service1Client server = new Service1Client();

        public AllExtrasApartements()
        {
            InitializeComponent();
            lvExtras.ItemsSource = server.GetTOExtrasApartements();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddManagerExtrasApartements());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (lvExtras.SelectedItem is DTOExtrasApartements item)
                NavigationService?.Navigate(new AddManagerExtrasApartements(item));
            else
                MessageBox.Show("יש לבחור רשומה לעריכה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (!(lvExtras.SelectedItem is DTOExtrasApartements item))
            {
                MessageBox.Show("יש לבחור רשומה למחיקה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (item.IdExtra == null || item.IdAapartment == null)
            {
                MessageBox.Show("נתונים לא תקינים", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool ok = server.DeleteExtrasApartement(item.IdExtra.IdExtra, item.IdAapartment.IdApartment);
            if (ok)
            {
                MessageBox.Show("נמחק בהצלחה!", "הצלחה", MessageBoxButton.OK, MessageBoxImage.Information);
                lvExtras.ItemsSource = server.GetTOExtrasApartements();
            }
            else
                MessageBox.Show("לא ניתן למחוק — קיימות רשומות מקושרות. מחק קודם את התלויות.", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
