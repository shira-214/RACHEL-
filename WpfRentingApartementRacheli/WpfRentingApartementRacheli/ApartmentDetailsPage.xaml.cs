using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfRentingApartementRacheli.ServiceReference1;

namespace WpfRentingApartementRacheli
{
    public partial class ApartmentDetailsPage : Page
    {
        DTOApartments apartment;
        Service1Client service = new Service1Client();
        List<DTOImages> images = new List<DTOImages>();
        int currentImageIndex;

        public ApartmentDetailsPage(DTOApartments apartment)
        {
            InitializeComponent();
            this.apartment = apartment;
            LoadDetails();
            LoadImages();
            LoadExtras();
        }

        private void LoadDetails()
        {
            string city = apartment.IdCities?.NameCity ?? "";
            string street = apartment.IdStreet?.StreetName ?? "";
            tbTitle.Text = city + " - " + street + " " + apartment.NumberHouse;
            tbFloor.Text = apartment.Floor.ToString();
            tbRooms.Text = apartment.NumberRooms.ToString();
            tbBeds.Text = apartment.NumberBeds.ToString();
            tbMinPrice.Text = apartment.MinimumPrice.ToString();
            tbExtraBed.Text = apartment.ExtraForBed.ToString();
            tbNote.Text = string.IsNullOrWhiteSpace(apartment.note) ? "אין הערות" : apartment.note;
        }

        private void LoadImages()
        {
            images = service.GetImages()
                .Where(x => x.Stataus &&
                            x.Image1 != null &&
                            x.Image1.Length > 0 &&
                            x.IdApartement != null &&
                            x.IdApartement.IdApartment == apartment.IdApartment)
                .OrderBy(x => x.NumImage)
                .ToList();

            if (images.Count == 0)
            {
                btnPrev.IsEnabled = false;
                btnNext.IsEnabled = false;
                tbImageCounter.Text = "אין תמונות";
                return;
            }

            currentImageIndex = 0;
            ShowCurrentImage();
        }

        private void ShowCurrentImage()
        {
            imgMain.Source = ImageManager.GetImage(images[currentImageIndex].Image1);
            tbImageCounter.Text = (currentImageIndex + 1) + " / " + images.Count;
            btnPrev.IsEnabled = currentImageIndex > 0;
            btnNext.IsEnabled = currentImageIndex < images.Count - 1;
        }

        private void LoadExtras()
        {
            var extras = service.GetTOExtrasApartements()
                .Where(x => x.Status &&
                            x.IdAapartment != null &&
                            x.IdAapartment.IdApartment == apartment.IdApartment &&
                            x.IdExtra != null)
                .Select(x => x.IdExtra.NameExtra)
                .ToList();

            if (extras.Count == 0)
                lbExtras.ItemsSource = new List<string> { "אין תוספות רשומות" };
            else
                lbExtras.ItemsSource = extras;
        }

        private void btnPrev_Click(object sender, RoutedEventArgs e)
        {
            if (currentImageIndex > 0)
            {
                currentImageIndex--;
                ShowCurrentImage();
            }
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (currentImageIndex < images.Count - 1)
            {
                currentImageIndex++;
                ShowCurrentImage();
            }
        }

        private void btnBook_Click(object sender, RoutedEventArgs e)
        {
            if (Global.currentHirers == null)
            {
                MessageBox.Show("יש להתחבר כשוכר כדי להזמין", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                NavigationService?.Navigate(new pageLogin());
                return;
            }

            NavigationService?.Navigate(new pageRenting(apartment));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new SearchW());
        }
    }
}
