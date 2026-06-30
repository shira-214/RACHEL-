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
    /// Interaction logic for pageRenting.xaml
    /// </summary>
    public partial class pageRenting : Page
    {
        DTOApartments apartment;
        Service1Client service = new Service1Client();
        int sumPay;
        public pageRenting(DTOApartments apartment)
        {
            InitializeComponent();
            this.apartment = apartment;
            sumPay = apartment.MinimumPrice;
            if (Global.selectedDate.Year != 0001)
            {
                dp.SelectedDate = Global.selectedDate;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DTORentings renting = new DTORentings();
            if (Global.currentHirers == null)
            {
                MessageBox.Show("לא התחברת");
                return;
            }
            renting.IdHirer = Global.currentHirers;
            renting.KodHapartment = apartment;
            if (dp.SelectedDate == null)
            {
                MessageBox.Show("לא נבחר תאריך");
                return;
            }
            
            renting.Date = dp.SelectedDate.Value;
            if (service.GetTORentings().FirstOrDefault(x => x.KodHapartment.IdApartment == apartment.IdApartment && x.Date.Date == renting.Date.Date) != null)
            {
                MessageBox.Show("הדירה תפוסה");
                return;
            }
            renting.SumPayment = sumPay;
            renting.SumBeds = Convert.ToInt32(txtNumBeds.Text);
            service.Addentings(renting);
            MessageBox.Show("ההזמנה נוספה בהצלחה!");
            NavigationService.Navigate(new SearchW());

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Global.currentHirers = service.GetTOHirers().FirstOrDefault(x => x.C_IDHirer == txtId.Text);
            //if (Global.currentHirers == null)
            //{
            //    MessageBox.Show("לא נמצא");
            //}
        }

        private void txtNumBeds_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            if (txtNumBeds.Text.Length > 0)
            {
                sumPay = apartment.MinimumPrice + apartment.NumberBeds * Convert.ToInt32(txtNumBeds.Text);
                tbSumToPay.Text = sumPay.ToString();
            }
            else
            {
                tbSumToPay.Text = apartment.MinimumPrice.ToString();

            }
        }
    }
}
