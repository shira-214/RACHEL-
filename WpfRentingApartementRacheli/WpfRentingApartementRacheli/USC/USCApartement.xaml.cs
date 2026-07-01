using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WpfRentingApartementRacheli.ServiceReference1;

namespace WpfRentingApartementRacheli.USC
{
    public partial class USCApartement : UserControl
    {
        Service1Client service = new Service1Client();
        DTOApartments apartment;

        public USCApartement(DTOApartments apartment)
        {
            InitializeComponent();
            this.apartment = apartment;
            DataContext = apartment;

            var imagesForAprtment = service.GetImages()
                .Where(x => x.IdApartement != null && x.IdApartement.IdApartment == apartment.IdApartment)
                .ToList();

            if (imagesForAprtment.Count > 0)
                image.Source = ImageManager.GetImage(imagesForAprtment[0].Image1);
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
