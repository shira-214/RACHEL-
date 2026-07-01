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
                .FirstOrDefault(x =>
                    x.IdApartement != null &&
                    x.IdApartement.IdApartment == apartment.IdApartment);

            if (apartmentImage != null)
                image.Source = ImageManager.GetImage(apartmentImage.Image1);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService(this);
            nav?.Navigate(new pageRenting(apartment));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string city = apartment.IdCities?.NameCity ?? "";
            string street = apartment.IdStreet?.StreetName ?? "";
            string details =
                $"עיר: {city}\n" +
                $"רחוב: {street} {apartment.NumberHouse}\n" +
                $"קומה: {apartment.Floor}\n" +
                $"חדרים: {apartment.NumberRooms}\n" +
                $"מיטות: {apartment.NumberBeds}\n" +
                $"מחיר התחלתי: {apartment.MinimumPrice} ש''ח\n" +
                $"תוספת למיטה: {apartment.ExtraForBed} ש''ח\n\n" +
                apartment.note;

            MessageBox.Show(details, "פרטי דירה", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
