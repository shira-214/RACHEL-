using System.Windows;
using System.Windows.Controls;
using WpfRentingApartementRacheli.ServiceReference1;

namespace WpfRentingApartementRacheli
{
    public partial class AllImages : Page
    {
        Service1Client server = new Service1Client();

        public AllImages()
        {
            InitializeComponent();
            lvImages.ItemsSource = server.GetImages();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AddManagerImages());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (lvImages.SelectedItem is DTOImages image)
                NavigationService?.Navigate(new AddManagerImages(image));
            else
                MessageBox.Show("יש לבחור תמונה לעריכה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (!(lvImages.SelectedItem is DTOImages image))
            {
                MessageBox.Show("יש לבחור תמונה למחיקה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (image.IdApartement == null)
            {
                MessageBox.Show("נתוני תמונה לא תקינים", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool ok = server.DeleteImage(image.IdApartement.IdApartment, image.NumImage);
            if (ok)
            {
                MessageBox.Show("נמחק בהצלחה!", "הצלחה", MessageBoxButton.OK, MessageBoxImage.Information);
                lvImages.ItemsSource = server.GetImages();
            }
            else
                MessageBox.Show("לא ניתן למחוק — קיימות רשומות מקושרות. מחק קודם את התלויות.", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
