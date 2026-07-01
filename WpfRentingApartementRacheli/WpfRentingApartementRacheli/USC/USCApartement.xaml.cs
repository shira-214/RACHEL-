using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WpfRentingApartementRacheli.ServiceReference1;

namespace WpfRentingApartementRacheli.USC
{
    public partial class USCApartement : UserControl
    {
        DTOApartments apartment;

        public USCApartement(DTOApartments apartment)
            : this(apartment, null)
        {
        }

        public USCApartement(DTOApartments apartment, IList<DTOImages> allImages)
        {
            InitializeComponent();
            this.apartment = apartment;
            DataContext = apartment;

            if (allImages == null)
            {
                var service = new Service1Client();
                allImages = service.GetImages();
            }

            var apartmentImage = allImages
                .Where(x => x.Stataus && x.Image1 != null && x.Image1.Length > 0)
                .Where(x =>
                    x.IdApartement != null &&
                    x.IdApartement.IdApartment == apartment.IdApartment)
                .OrderBy(x => x.NumImage)
                .FirstOrDefault();

            if (apartmentImage != null)
                image.Source = ImageManager.GetImage(apartmentImage.Image1);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Global.currentHirers == null)
            {
                MessageBox.Show("יש להתחבר כשוכר כדי להזמין", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                NavigationService nav = NavigationService.GetNavigationService(this);
                nav?.Navigate(new pageLogin());
                return;
            }

            NavigationService nav2 = NavigationService.GetNavigationService(this);
            nav2?.Navigate(new pageRenting(apartment));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav?.Navigate(new ApartmentDetailsPage(apartment));
        }
    }
}
