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
    /// Interaction logic for AddManagerApartments.xaml
    /// </summary>
    public partial class AddManagerApartments : Page
    {
        Service1Client server = new Service1Client();
        DTOApartments Apartment;//אוביקט מסוג משתמש עליו עובדים
        bool Add;
        public AddManagerApartments()
        {
            InitializeComponent();
            Apartment = new DTOApartments();
            this.DataContext = Apartment;
            Add = true;
            ApartmentCities.ItemsSource = server.GetTOCities();

        }

        public AddManagerApartments(DTOApartments Apartment)
        {
            InitializeComponent();
            this.Apartment = Apartment;
            this.DataContext = Apartment;
            Add = false;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //בדיקה על הבדיקות תקינות
            if (Validation.GetHasError(ApartmentsNameOwner) == true || Validation.GetHasError(ApartmentCities) == true || Validation.GetHasError(ApartmentStreetsNames) == true || Validation.GetHasError(txtNumberHouse) == true || Validation.GetHasError(txtFloor)  == true  || Validation.GetHasError(txtNumberRooms) == true || Validation.GetHasError(txtNumberBeds) == true || Validation.GetHasError(txtMinimumPrice) == true || Validation.GetHasError(txtExtraForBed) == true || Validation.GetHasError(txtnote) == true || Validation.GetHasError(txtPhoneOwner) == true)
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
                    if (Add == true)
                    { server.AddApartments(Apartment); }
                    else
                    {
                        {
                            if (server.UpdateApartments(Apartment))
                                MessageBox.Show("עודכן בהצלחה!!😀😀😀😀😀");
                            else
                                MessageBox.Show("שגיאה בעדכון", "", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    }
                    NavigationService.Navigate(new Image());
                }
            }

        }

        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    byte[] arr = ImageManager.UploadImage_Dlg();
        //    //מגדירים את המקור של הפקד של התמונה שכתבנו בסעיף הקודם שיהיה התמונה שנבחרה           
        //    image.Source = ImageManager.GetImage(arr);
        //    //מבצעים השמה לתכונה תמונה של האוביקט אותו רוצים להוסיף לפי ה די טי או
        //    user.image = arr;//כי מקושר לי.... מה לעשות כאן??


        //}
    }

}
