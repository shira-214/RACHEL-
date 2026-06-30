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
    /// Interaction logic for LogInApartement.xaml
    /// </summary>
    public partial class LogInApartement : Page
    {
        Service1Client server = new Service1Client();

        public LogInApartement()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Global.currentApartments = server.GetApartments().FirstOrDefault(x => x.NameOwner == txtHirerName.Text && x.PhoneOwner == txtPassword.Password);
            if (Global.currentApartments != null)
            {
                NavigationService.Navigate(new MainWindow());
            }
            else
            {
                MessageBox.Show("הנך מועבר לדף הרישום |  משתמש לא נמצא");
                Global.isManager = false;
                NavigationService.Navigate(new AddApartement());/*לשנות להוספת משכיר!!!*/
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Global.isManager = false;
            NavigationService.Navigate(new AddApartement());/*לשנות להוספת שוכר!!*/

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }
    }
}
