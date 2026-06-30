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


namespace WpfRentingApartementRacheli
{
    /// <summary>
    /// Interaction logic for AddW.xaml
    /// </summary>
    public partial class AddW : Page
    {
        Service1Client server = new Service1Client();
        DTOApartments Apartment;//אוביקט מסוג משתמש עליו עובדים
        bool Add;

        public AddW()
        {
            InitializeComponent();
            Apartment = new DTOApartments();
            this.DataContext = Apartment;
            Add = true;
           ApartmentCities.ItemsSource = server.GetTOCities();

        }

        private void ShowLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginPanel.Visibility = Visibility.Visible; // מציג התחברות
            RegisterPanel.Visibility = Visibility.Collapsed; // מסתיר הרשמה

        }

        // מעבר למסך הרשמה
        private void ShowRegister_Click(object sender, RoutedEventArgs e)
        {
            LoginPanel.Visibility = Visibility.Collapsed;
            RegisterPanel.Visibility = Visibility.Visible;

            // עדכון צבעי הכפתורים
        }

        // לחיצה על כפתור סיום הרשמה
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            // כאן תכניסי את הקוד ששומר את המשתמש החדש
            if (Validation.GetHasError(ApartmentsNameOwner) == true || Validation.GetHasError(ApartmentCities) == true || Validation.GetHasError(ApartmentStreetsNames) == true || Validation.GetHasError(txtNumberHouse) == true || Validation.GetHasError(txtFloor) == true || Validation.GetHasError(txtNumberRooms) == true || Validation.GetHasError(txtNumberBeds) == true || Validation.GetHasError(txtMinimumPrice) == true || Validation.GetHasError(txtExtraForBed) == true || Validation.GetHasError(txtnote) == true || Validation.GetHasError(txtPhoneOwner) == true)
            {
                MessageBox.Show("נתונים שגויים!");
            }
            else
            {
                //בדיקה שאין נתונים ריקים
                if (ApartmentsNameOwner.Text == "" || ApartmentCities.SelectedItem == null || ApartmentStreetsNames.SelectedItem == null || (txtNumberHouse.Text == "") || (txtFloor.Text == "") || (txtNumberRooms.Text == "") || (txtNumberBeds.Text == "") || txtMinimumPrice.Text == "" || txtExtraForBed.Text == "" || txtnote.Text == "" || txtPhoneOwner.Text == "")
                {
                    MessageBox.Show("נתונים חסרים!");
                }
                else
                {
                    Apartment.Status = true;
                    server.AddApartments(Apartment);
                    NavigationService.Navigate(new AllApartments());
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}


