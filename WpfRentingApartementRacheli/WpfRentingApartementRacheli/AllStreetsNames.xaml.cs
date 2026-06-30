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
    /// Interaction logic for AllStreetsNames.xaml
    /// </summary>
    public partial class AllStreetsNames : Page
    {
        public AllStreetsNames()
        {
            InitializeComponent();
            //lvStreetNames.ItemsSource = server.GET();//למה הוא לא נותן לעשות ליסט ויו?

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new AddManagerStreetNames());

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //if (lvs.SelectedItem != null)
            //{
            //    NavigationService.Navigate(new AddManagerStreetNames(lvImages.SelectedItem as DTOImages));
            //}

        }
    }
}
