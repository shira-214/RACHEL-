using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfRentingApartementRacheli.ServiceReference1;

namespace WpfRentingApartementRacheli
{
    public partial class POrders : Page
    {
        Service1Client server = new Service1Client();
        DTOApartments apartment;
        byte[] selectedImage;

        public POrders(DTOApartments apartment)
        {
            InitializeComponent();
            this.apartment = apartment;

            if (Global.CurrentRole != Global.UserRole.Owner && Global.CurrentRole != Global.UserRole.Manager)
            {
                MessageBox.Show("אין הרשאה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                NavigationService?.Navigate(new LandingPage());
                return;
            }

            string city = apartment.IdCities?.NameCity ?? "";
            string street = apartment.IdStreet?.StreetName ?? "";
            tbApartment.Text = city + " - " + street + " " + apartment.NumberHouse;
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            selectedImage = ImageManager.UploadImage_Dlg();
            if (selectedImage != null)
                imagePreview.Source = ImageManager.GetImage(selectedImage);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (selectedImage == null)
            {
                MessageBox.Show("יש לבחור תמונה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int nextNum = server.GetImages()
                .Where(x => x.IdApartement != null && x.IdApartement.IdApartment == apartment.IdApartment)
                .Select(x => x.NumImage)
                .DefaultIfEmpty(0)
                .Max() + 1;

            DTOImages imageDto = new DTOImages
            {
                IdApartement = apartment,
                NumImage = nextNum,
                Image1 = selectedImage,
                Stataus = true
            };

            if (server.AddImages(imageDto))
            {
                MessageBox.Show("התמונה נשמרה בהצלחה!", "הצלחה", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigateBack();
            }
            else
            {
                MessageBox.Show("שגיאה בשמירת התמונה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigateBack();
        }

        private void NavigateBack()
        {
            if (Global.CurrentRole == Global.UserRole.Owner)
                NavigationService?.Navigate(new OwnerDashboardPage());
            else
                NavigationService?.Navigate(new AllApartments());
        }
    }
}
