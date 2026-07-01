using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfRentingApartementRacheli.ServiceReference1;

namespace WpfRentingApartementRacheli
{
    public partial class AddManagerImages : Page
    {
        Service1Client server = new Service1Client();
        DTOImages imageDto;
        byte[] selectedImage;
        bool add;

        public AddManagerImages()
        {
            InitializeComponent();
            add = true;
            cmbApartment.ItemsSource = server.GetApartments();
        }

        public AddManagerImages(DTOApartments apartment)
        {
            InitializeComponent();
            add = true;
            cmbApartment.ItemsSource = server.GetApartments();
            if (apartment != null)
            {
                cmbApartment.SelectedItem = server.GetApartments()
                    .FirstOrDefault(a => a.IdApartment == apartment.IdApartment);
                cmbApartment.IsEnabled = false;
            }
            LoadExistingImages();
        }

        public AddManagerImages(DTOImages existing)
        {
            InitializeComponent();
            add = false;
            imageDto = existing;
            tbTitle.Text = "עריכת תמונה";
            btnEnd.Content = "עדכן תמונה";

            cmbApartment.ItemsSource = server.GetApartments();
            if (existing.IdApartement != null)
            {
                cmbApartment.SelectedItem = server.GetApartments()
                    .FirstOrDefault(a => a.IdApartment == existing.IdApartement.IdApartment);
            }
            cmbApartment.IsEnabled = false;

            selectedImage = existing.Image1;
            if (selectedImage != null)
                imagePreview.Source = ImageManager.GetImage(selectedImage);

            chkStatus.IsChecked = existing.Stataus;
            LoadExistingImages();
        }

        private void LoadExistingImages()
        {
            if (!(cmbApartment.SelectedItem is DTOApartments apartment))
            {
                lvExistingImages.ItemsSource = null;
                return;
            }

            lvExistingImages.ItemsSource = server.GetImages()
                .Where(x => x.IdApartement != null && x.IdApartement.IdApartment == apartment.IdApartment)
                .OrderBy(x => x.NumImage)
                .ToList();
        }

        private void ClearNewImageForm()
        {
            selectedImage = null;
            imagePreview.Source = null;
            chkStatus.IsChecked = true;
        }

        private void cmbApartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (add)
                LoadExistingImages();
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

            bool ok;
            if (add)
            {
                int nextNum = server.GetImages()
                    .Where(x => x.IdApartement != null && x.IdApartement.IdApartment == apartment.IdApartment)
                    .Select(x => x.NumImage)
                    .DefaultIfEmpty(0)
                    .Max() + 1;

                imageDto = new DTOImages
                {
                    IdApartement = new DTOApartments { IdApartment = apartment.IdApartment },
                    NumImage = nextNum,
                    Image1 = selectedImage,
                    Stataus = chkStatus.IsChecked == true
                };
                ok = server.AddImages(imageDto);
            }
            else
            {
                imageDto.IdApartement = new DTOApartments { IdApartment = apartment.IdApartment };
                imageDto.Image1 = selectedImage;
                imageDto.Stataus = chkStatus.IsChecked == true;
                ok = server.UpdateImages(imageDto);
            }

            if (ok)
            {
                MessageBox.Show(add ? "התמונה נשמרה בהצלחה!" : "התמונה עודכנה בהצלחה!",
                    "הצלחה", MessageBoxButton.OK, MessageBoxImage.Information);
                if (add)
                {
                    ClearNewImageForm();
                    LoadExistingImages();
                }
                else
                    NavigationService?.Navigate(new AllImages());
            }
            else
                MessageBox.Show("שגיאה בשמירת התמונה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btnAddAnother_Click(object sender, RoutedEventArgs e)
        {
            ClearNewImageForm();
        }
    }
}
