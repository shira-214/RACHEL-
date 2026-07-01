using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfRentingApartementRacheli.ServiceReference1;

namespace WpfRentingApartementRacheli
{
    /// <summary>
    /// Interaction logic for AllAraesCitiesStreet.xaml
    /// </summary>
    public partial class AllAraesCitiesStreet : Page
    {
        Service1Client server = new Service1Client();

        public AllAraesCitiesStreet()
        {
            InitializeComponent();
            lvAraesCitiesStreet.ItemsSource = server.GetTOAraesCitiesStreets();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new AddManagerAraesCitiesStreet());

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (lvAraesCitiesStreet.SelectedItem is DTOAraesCitiesStreet link)
                NavigationService?.Navigate(new AddManagerAraesCitiesStreet(link));
            else
                MessageBox.Show("יש לבחור רשומה לעדכון", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (!(lvAraesCitiesStreet.SelectedItem is DTOAraesCitiesStreet link))
            {
                MessageBox.Show("יש לבחור רשומה למחיקה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (link.IdStreetDTo == null || link.IdCities == null || link.IdArea == null)
            {
                MessageBox.Show("נתונים לא תקינים", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool ok = server.DeleteAraesCitiesStreet(
                link.IdStreetDTo.IdStreet,
                link.IdCities.IdCity,
                link.IdArea.IdArea);

            if (ok)
            {
                MessageBox.Show("נמחק בהצלחה!", "הצלחה", MessageBoxButton.OK, MessageBoxImage.Information);
                lvAraesCitiesStreet.ItemsSource = server.GetTOAraesCitiesStreets();
            }
            else
                MessageBox.Show("לא ניתן למחוק — קיימות רשומות מקושרות. מחק קודם את התלויות.", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
