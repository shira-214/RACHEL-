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
    /// Interaction logic for AllExtrasApartements.xaml
    /// </summary>
    public partial class AllExtrasApartements : Page
    {
        public AllExtrasApartements()
        {
            InitializeComponent();
            //lvRentings.ItemsSource = server.GetTORentings();לא עובדד באסה

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService nav = NavigationService.GetNavigationService(this);
            //nav.Navigate(new ADDEX());

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //if (.SelectedItem != null)
            //{
            //    NavigationService.Navigate(new AddManagerApartments(lvApartment.SelectedItem as DTOApartments));
            //}

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
