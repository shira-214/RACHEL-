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
    /// Interaction logic for AllExtras.xaml
    /// </summary>
    public partial class AllExtras : Page
    {
        Service1Client server = new Service1Client();

        public AllExtras()
        {
            InitializeComponent();
            lvExtras.ItemsSource = server.GetExtras();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new AddManagerExtras());

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (lvExtras.SelectedItem != null)
            {
                NavigationService.Navigate(new AddManagerExtras(lvExtras.SelectedItem as DTOExtras));
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (!(lvExtras.SelectedItem is DTOExtras extra))
            {
                MessageBox.Show("יש לבחור תוספת למחיקה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool ok = server.DeleteExtra(extra.IdExtra);
            if (ok)
            {
                MessageBox.Show("נמחק בהצלחה!", "הצלחה", MessageBoxButton.OK, MessageBoxImage.Information);
                lvExtras.ItemsSource = server.GetExtras();
            }
            else
                MessageBox.Show("לא ניתן למחוק — קיימות רשומות מקושרות. מחק קודם את התלויות.", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void lvExtras_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
