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
    /// Interaction logic for pageLogin.xaml
    /// </summary>
    public partial class pageLogin : Page
    {
        Service1Client server = new Service1Client();
        public pageLogin()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Global.currentHirers = server.GetTOHirers().FirstOrDefault(x => x.C_IDHirer == txtId.Text);
            if (Global.currentHirers != null)
            {
                NavigationService.Navigate(new SearchW());
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("אינך רשום במערכת, האם ברצונך להירשם כעת?", "משתמש לא נמצא", MessageBoxButton.OKCancel, MessageBoxImage.Information);

                if (result == MessageBoxResult.OK)
                {
                    Global.isManager = false;
                    NavigationService.Navigate(new AddManagerHirers());
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Global.isManager = false;
            NavigationService.Navigate(new AddManagerHirers());

        }
    }
}
