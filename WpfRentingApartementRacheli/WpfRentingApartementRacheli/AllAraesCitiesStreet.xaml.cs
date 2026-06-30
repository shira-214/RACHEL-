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
            //if (lvAraesCitiesStreet.SelectedItem != null)למה לא עובד???
            //{
            //    NavigationService.Navigate(new AddManagerAraesCitiesStreet(lvAraesCitiesStreet.SelectedItem as DTOAraesCitiesStreet));
            //}

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
