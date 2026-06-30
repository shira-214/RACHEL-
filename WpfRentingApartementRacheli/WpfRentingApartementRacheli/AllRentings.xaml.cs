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
    /// Interaction logic for AllRentings.xaml
    /// </summary>
    public partial class AllRentings : Page
    {
        Service1Client server = new Service1Client();

        public AllRentings()
        {
            InitializeComponent();
            lvRentings.ItemsSource = server.GetTORentings();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           //לא עשיתי כי לא נראה לי שצריך להוסיף
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
