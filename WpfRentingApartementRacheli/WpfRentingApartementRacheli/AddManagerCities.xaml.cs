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
    /// Interaction logic for AddManagerCities.xaml
    /// </summary>
    public partial class AddManagerCities : Page
    {
        Service1Client server = new Service1Client();
        DTOCities city;//אוביקט מסוג משתמש עליו עובדים
        bool add;
        public AddManagerCities()
        {
            InitializeComponent();
            city = new DTOCities();
            this.DataContext = city;
            add = true;

        }

        public AddManagerCities(DTOAreas Area)
        {
            InitializeComponent();
            this.city = city;
            this.DataContext = Area;
            add = false;

        }


        public void btnEnd_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(txtNameCity))
            {
                MessageBox.Show("נתונים שגויים!");
            }
            else
            {
                //בדיקה שאין נתונים ריקים
                if (txtNameCity.Text == "")
                {
                    MessageBox.Show("נתונים חסרים!");
                }
                else
                {
                    if (add == true)
                    { server.AddCities(city); }
                    else
                    {
                        {
                            if (server.UpdateCities(city))
                                MessageBox.Show("עודכן בהצלחה!!😀😀😀😀😀");
                            else
                                MessageBox.Show("שגיאה בעדכון", "", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    }
                    NavigationService.Navigate(new AllAreas());
                }
            }

        }

    }

}

