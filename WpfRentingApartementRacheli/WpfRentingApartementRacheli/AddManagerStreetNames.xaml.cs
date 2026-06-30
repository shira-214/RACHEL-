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
    /// Interaction logic for AddManagerStreetNames.xaml
    /// </summary>
    public partial class AddManagerStreetNames : Page
    {
        Service1Client server = new Service1Client();
        DTOStreetsNames streetsNames;//אוביקט מסוג משתמש עליו עובדים
        bool Add;

        public AddManagerStreetNames()
        {
            InitializeComponent();
            streetsNames = new DTOStreetsNames();
            this.DataContext = streetsNames;
            Add = true;


        }
        public AddManagerStreetNames(DTOStreetsNames streetsNames)
        {
            InitializeComponent();
            this.streetsNames = streetsNames;
            this.DataContext = streetsNames;
            Add = false;

        }


        private void btnEnd_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(txtStreet))
            {
                MessageBox.Show("נתונים שגויים!");
            }
            else
            {
                //בדיקה שאין נתונים ריקים
                if (txtStreet.Text == "")
                {
                    MessageBox.Show("נתונים חסרים!");
                }
                else
                {
                    if (Add == true)
                    {
                        MessageBox.Show("נתונים חסרים!");
                    }

                    //{ server.AddStreetsNames(txtStreet); }
                    else
                    {
                        {
                            if (server.UpdateStreetsNames(streetsNames))//למה אין לו אקסטרה ???
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
