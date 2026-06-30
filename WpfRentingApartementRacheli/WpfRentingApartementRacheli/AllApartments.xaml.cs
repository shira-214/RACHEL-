using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
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
    /// Interaction logic for AllApartments.xaml
    /// </summary>
    public partial class AllApartments : Page
    {
        Service1Client server = new Service1Client();
        public AllApartments()
        {
            InitializeComponent();
            lvApartment.ItemsSource = server.GetApartments();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new AddManagerApartments());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (lvApartment.SelectedItem != null)
            {
                NavigationService.Navigate(new AddManagerApartments(lvApartment.SelectedItem as DTOApartments));
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
