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

            if (Global.CurrentRole != Global.UserRole.Owner && Global.CurrentRole != Global.UserRole.Manager)
            {
                MessageBox.Show("אין הרשאה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                NavigationService?.Navigate(new LandingPage());
                return;
            }

            this.apartment = ResolveApartment(apartment);
            if (this.apartment == null || this.apartment.IdApartment <= 0)
            {
                MessageBox.Show("לא נמצא מזהה דירה תקין. שמרי קודם את הדירה ונסי שוב.",
                    "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                NavigationService?.Navigate(new OwnerDashboardPage());
                return;
            }

            string city = this.apartment.IdCities?.NameCity ?? "";
            string street = this.apartment.IdStreet?.StreetName ?? "";
            tbApartment.Text = city + " - " + street + " " + this.apartment.NumberHouse;
            LoadExistingImages();
        }

        private void LoadExistingImages()
        {
            lvExistingImages.ItemsSource = server.GetImages()
                .Where(x => x.IdApartement != null && x.IdApartement.IdApartment == apartment.IdApartment)
                .OrderBy(x => x.NumImage)
                .ToList();
        }

        private void ClearNewImageForm()
        {
            selectedImage = null;
            imagePreview.Source = null;
        }

        private DTOApartments ResolveApartment(DTOApartments apt)
        {
            if (apt == null)
                return null;

            if (apt.IdApartment > 0)
            {
                var byId = server.GetApartments().FirstOrDefault(a => a.IdApartment == apt.IdApartment);
                if (byId != null)
                    return byId;
            }

            return server.GetApartments().FirstOrDefault(a =>
                a.NameOwner == apt.NameOwner &&
                a.PhoneOwner == apt.PhoneOwner &&
                a.NumberHouse == apt.NumberHouse &&
                a.IdCities != null && apt.IdCities != null &&
                a.IdCities.IdCity == apt.IdCities.IdCity &&
                a.IdStreet != null && apt.IdStreet != null &&
                a.IdStreet.IdStreet == apt.IdStreet.IdStreet);
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

            apartment = ResolveApartment(apartment);
            if (apartment == null || apartment.IdApartment <= 0)
            {
                MessageBox.Show("לא נמצא מזהה דירה תקין לשמירת התמונה",
                    "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int nextNum = server.GetImages()
                .Where(x => x.IdApartement != null && x.IdApartement.IdApartment == apartment.IdApartment)
                .Select(x => x.NumImage)
                .DefaultIfEmpty(0)
                .Max() + 1;

            DTOImages imageDto = new DTOImages
            {
                IdApartement = new DTOApartments { IdApartment = apartment.IdApartment },
                NumImage = nextNum,
                Image1 = selectedImage,
                Stataus = true
            };

            try
            {
                if (server.AddImages(imageDto))
                {
                    MessageBox.Show("התמונה נשמרה בהצלחה!", "הצלחה",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearNewImageForm();
                    LoadExistingImages();
                }
                else
                {
                    MessageBox.Show("שגיאה בשמירת התמונה. ודאי ש-Host.exe רץ.",
                        "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("שגיאה בשמירת התמונה:\n" + ex.Message,
                    "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
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
