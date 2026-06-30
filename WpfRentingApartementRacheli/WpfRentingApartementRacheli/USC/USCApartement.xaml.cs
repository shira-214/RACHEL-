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



namespace WpfRentingApartementRacheli.USC
{
    /// <summary>
    /// Interaction logic for USCApartement.xaml
    /// </summary>
    public partial class USCApartement : UserControl
    {
        Service1Client service = new Service1Client();
        DTOApartments apartment;
        List<DTOImages> imagesForAprtment;
        public USCApartement(DTOApartments apartment)
        {
            InitializeComponent();
            this.apartment = apartment;
            this.DataContext = apartment;
            imagesForAprtment = service.GetImages().Where(x => x.IdApartement.IdApartment == apartment.IdApartment).ToList();

            if (imagesForAprtment.Count > 0)
            {
                image.Source = ImageManager.GetImage(imagesForAprtment[0].Image1);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav.Navigate(new pageRenting(apartment));

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
