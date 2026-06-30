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
    /// Interaction logic for AddManagerHirers.xaml
    /// </summary>
    public partial class AddManagerHirers : Page

    {
        Service1Client server = new Service1Client();
        DTOHirers hirer;//אוביקט מסוג משתמש עליו עובדים
        bool Add;

        public AddManagerHirers()
        {
            InitializeComponent();
            hirer = new DTOHirers();
            this.DataContext = hirer;
            Add = true;

        }

        public AddManagerHirers(DTOHirers hirers)
        {
            InitializeComponent();
            this.hirer = hirers;
            this.DataContext = hirer;
            Add = false;

        }
        private void btnEnd_Click(object sender, RoutedEventArgs e)
        {
            if (Validation.GetHasError(txtExtras) || Validation.GetHasError(txtFhone))
            {
                MessageBox.Show("נתונים שגויים!");
            }
            else
            {
                //בדיקה שאין נתונים ריקים
                if (txtExtras.Text == "" || txtFhone.Text == "")
                {
                    MessageBox.Show("נתונים חסרים!");
                }
                else
                {
                    if (Add == true)
                    {
                        if (server.GetTOHirers().FirstOrDefault(x => x.C_IDHirer == hirer.C_IDHirer) != null)
                        {
                            MessageBox.Show("שוכר קיים");
                            return;
                        }
                        server.AddHirers(hirer);
                        if (Global.isManager == false)
                        {
                            Global.currentHirers = server.GetTOHirers().Last();
                            NavigationService.Navigate(new SearchW());

                        }
                        else
                        {
                            NavigationService.Navigate(new AllHirers());
                        }
                    }
                    else
                    {
                        {
                            if (server.UpdateHirers(hirer))
                                MessageBox.Show("עודכן בהצלחה!!😀😀😀😀😀");
                            else
                                MessageBox.Show("שגיאה בעדכון", "", MessageBoxButton.OK, MessageBoxImage.Error);    
                            NavigationService.Navigate(new AllAreas());

                        }

                    }
                }
            }

        }
    }
}
