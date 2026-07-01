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
    /// Interaction logic for AddManagerExtras.xaml
    /// </summary>
    public partial class AddManagerExtras : Page
    {
        Service1Client server = new Service1Client();
        DTOExtras Extra;//אוביקט מסוג משתמש עליו עובדים
        bool Add;
        public AddManagerExtras()
        {
            InitializeComponent();
            Extra = new DTOExtras();
            this.DataContext = Extra;
            Add = true;

        }

        public AddManagerExtras(DTOExtras extra)
        {
            InitializeComponent();
            this.Extra = extra;
            this.DataContext = extra;
            Add = false;

        }


        private void btnEnd_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(txtExtras))
            {
                MessageBox.Show("נתונים שגויים!");
            }
            else
            {
                //בדיקה שאין נתונים ריקים
                if (txtExtras.Text == "")
                {
                    MessageBox.Show("נתונים חסרים!");
                }
                else
                {
                    if (Add == true)
                    {
                        if (server.AddExtras(Extra))
                            MessageBox.Show("נוסף בהצלחה!", "הצלחה", MessageBoxButton.OK, MessageBoxImage.Information);
                        else
                        {
                            MessageBox.Show("שגיאה בהוספה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                    else
                    {
                        if (server.Updatextras(Extra))
                            MessageBox.Show("עודכן בהצלחה!", "הצלחה", MessageBoxButton.OK, MessageBoxImage.Information);
                        else
                        {
                            MessageBox.Show("שגיאה בעדכון", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                    NavigationService.Navigate(new AllExtras());
                }
            }

        }
    }
}
