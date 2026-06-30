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
    /// Interaction logic for AllCities.xaml
    /// </summary>
    public partial class AllCities : Page

    {
        Service1Client server = new Service1Client();

        public AllCities()
        {
            InitializeComponent();
            lvCities.ItemsSource = server.GetTOCities();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new AddManagerCities());

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //if (lvCities.SelectedItem != null)
            //{
            //    NavigationService.Navigate(new AddManagerCities(lvCities.SelectedItem as DTOCities));אווף לא עובד
            //}

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
