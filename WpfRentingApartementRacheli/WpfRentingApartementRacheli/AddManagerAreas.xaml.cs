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
    /// Interaction logic for AddManagerAreas.xaml
    /// </summary>
    public partial class AddManagerAreas : Page
    {
        Service1Client server = new Service1Client();
        DTOAreas Area;//אוביקט מסוג משתמש עליו עובדים
        bool Add;

        public AddManagerAreas()
        {
            InitializeComponent();
            Area = new DTOAreas();
            this.DataContext = Area;
            Add = true;

        }
        public AddManagerAreas(DTOAreas Area)
        {
            InitializeComponent();
            this.Area = Area;
            this.DataContext = Area;
            Add = false;

        }


        private void btnEnd_Click(object sender, RoutedEventArgs e)
        {
            if (  Validation.GetHasError(txtNameArea))
            {
                MessageBox.Show("נתונים שגויים!");
            }
            else
            {
                //בדיקה שאין נתונים ריקים
                if (txtNameArea.Text == "" )
                {
                    MessageBox.Show("נתונים חסרים!");
                }
                else
                {
                    if (Add == true)
                    { server.AddArea(Area); }
                    else
                    {
                        {
                            if (server.UpdateArae(Area))
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
