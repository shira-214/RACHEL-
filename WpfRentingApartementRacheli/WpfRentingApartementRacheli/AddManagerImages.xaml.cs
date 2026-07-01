using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfRentingApartementRacheli.ServiceReference1;

namespace WpfRentingApartementRacheli
{
    public partial class AddManagerImages : Page
    {
        Service1Client server = new Service1Client();
        byte[] selectedImage;

        public AddManagerImages()
        {
            InitializeComponent();
            cmbApartment.ItemsSource = server.GetApartments();
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            selectedImage = ImageManager.UploadImage_Dlg();
            if (selectedImage != null)
                imagePreview.Source = ImageManager.GetImage(selectedImage);
        }

        private void btnEnd_Click(object sender, RoutedEventArgs e)
        {
            if (!(cmbApartment.SelectedItem is DTOApartments apartment))
            {
                MessageBox.Show("יש לבחור דירה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

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
                NavigationService?.Navigate(new AllImages());
            }
            else
                MessageBox.Show("שגיאה בשמירת התמונה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
