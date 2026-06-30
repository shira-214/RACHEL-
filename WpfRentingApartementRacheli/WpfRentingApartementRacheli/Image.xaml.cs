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
using WpfRentingApartementRacheli.USC;

namespace WpfRentingApartementRacheli
{
    /// <summary>
    /// Interaction logic for POrders.xaml
    /// </summary>
    public partial class POrders : Page
    {
        Service1Client server=new Service1Client(); 
        public POrders()
        {
            InitializeComponent();
            createUC();

        }
        private void createUC()
        {
            foreach (var item in server.GetApartments())
            {
                USCApartement uc = new USCApartement(item);
                wrp.Children.Add(uc);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)//לשאול את המורה אם לא לעשות את הבדיקןת תקינו לפני
        {
            NavigationService.Navigate(new AllApartments());

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Image());//לשאול את המורה איך שומרים תמונה

        }
    }
}
