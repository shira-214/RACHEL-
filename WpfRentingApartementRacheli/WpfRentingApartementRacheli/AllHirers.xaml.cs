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
    /// Interaction logic for AllHirers.xaml
    /// </summary>
    public partial class AllHirers : Page
    {
        Service1Client server = new Service1Client();

        public AllHirers()
        {
            InitializeComponent();
            lvHirers.ItemsSource = server.GetTOHirers();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new AddManagerHirers());

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (lvHirers.SelectedItem != null)
            {
                NavigationService.Navigate(new AddManagerHirers(lvHirers.SelectedItem as DTOHirers));
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (!(lvHirers.SelectedItem is DTOHirers hirer))
            {
                MessageBox.Show("יש לבחור שוכר למחיקה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool ok = server.DeleteHirer(hirer.C_IDHirer);
            if (ok)
            {
                MessageBox.Show("נמחק בהצלחה!", "הצלחה", MessageBoxButton.OK, MessageBoxImage.Information);
                lvHirers.ItemsSource = server.GetTOHirers();
            }
            else
                MessageBox.Show("לא ניתן למחוק — קיימות רשומות מקושרות (הזמנות). מחק קודם את התלויות.", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
