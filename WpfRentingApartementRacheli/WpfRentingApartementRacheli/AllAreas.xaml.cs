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
    /// Interaction logic for AllAreas.xaml
    /// </summary>
    public partial class AllAreas : Page
    {
        Service1Client server = new Service1Client();

        public AllAreas()
        {
            InitializeComponent();
            lvArea.ItemsSource = server.GetAreas();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new AddManagerAreas());

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (lvArea.SelectedItem != null)
            {
                NavigationService.Navigate(new AddManagerAreas(lvArea.SelectedItem as DTOAreas));
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
