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

namespace WpfRentingApartementRacheli
{
    /// <summary>
    /// Interaction logic for ManagerPage.xaml
    /// </summary>
    public partial class ManagerPage : Page
    {
        public ManagerPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ManagerFramre.Navigate(new AllApartments());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ManagerFramre.Navigate(new AllAraesCitiesStreet());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ManagerFramre.Navigate(new AllAreas());

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ManagerFramre.Navigate(new AllCities());

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            ManagerFramre.Navigate(new AllExtras());

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            ManagerFramre.Navigate(new AllExtrasApartements());

        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            ManagerFramre.Navigate(new AllHirers());

        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            ManagerFramre.Navigate(new AllImages());

        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            ManagerFramre.Navigate(new AllRentings());

        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            ManagerFramre.Navigate(new AllStreetsNames());

        }
    }
}
