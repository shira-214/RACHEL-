using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfRentingApartementRacheli.ServiceReference1;

namespace WpfRentingApartementRacheli
{
    public partial class pagePayment : Page
    {
        DTOApartments apartment;
        int beds;
        DateTime rentDate;
        int sumPay;
        Service1Client service = new Service1Client();

        public pagePayment(DTOApartments apartment, int beds, DateTime rentDate, int sumPay)
        {
            InitializeComponent();

            if (Global.currentHirers == null)
            {
                MessageBox.Show("יש להתחבר כשוכר", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                NavigationService?.Navigate(new pageLogin());
                return;
            }

            this.apartment = apartment;
            this.beds = beds;
            this.rentDate = rentDate.Date;
            this.sumPay = sumPay;

            string city = apartment.IdCities?.NameCity ?? "";
            string street = apartment.IdStreet?.StreetName ?? "";
            tbOrderInfo.Text =
                city + " - " + street + " " + apartment.NumberHouse + "\n" +
                "תאריך: " + rentDate.ToString("dd/MM/yyyy") + "\n" +
                "מיטות: " + beds + "\n" +
                "סכום: " + sumPay + " ש''ח";

            txtName.Text = Global.currentHirers.NameHirer ?? "";
            txtId.Text = Global.currentHirers.C_IDHirer ?? "";
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (Global.currentHirers == null)
            {
                MessageBox.Show("לא התחברת", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                NavigationService?.Navigate(new pageLogin());
                return;
            }

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("יש להזין שם", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (beds <= 0 || beds > apartment.NumberBeds)
            {
                MessageBox.Show("מספר מיטות לא תקין", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!IsValidRentDate(rentDate))
            {
                MessageBox.Show("התאריך שנבחר אינו זמין", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!IsCreditCardValid(txtCard.Text) || !IsCvvValid(txtCvv.Text) || !IsExpiryValid(txtExpiry.Text))
            {
                MessageBox.Show("פרטי אשראי לא תקינים", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Global.currentHirers.NameHirer = txtName.Text.Trim();

            DTORentings renting = new DTORentings
            {
                IdHirer = Global.currentHirers,
                KodHapartment = apartment,
                Date = rentDate,
                SumPayment = sumPay,
                SumBeds = beds
            };

            if (service.Addentings(renting))
            {
                MessageBox.Show("ההזמנה נשלחה!", "הצלחה", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService?.Navigate(new SearchW());
            }
            else
                MessageBox.Show("שגיאה בשמירת ההזמנה. ודאי ש-Host.exe רץ.", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private bool IsValidRentDate(DateTime date)
        {
            if (date.Date < DateTime.Today)
                return false;
            if (date.DayOfWeek != DayOfWeek.Saturday)
                return false;

            return !service.GetTORentings().Any(x =>
                x.KodHapartment != null &&
                x.KodHapartment.IdApartment == apartment.IdApartment &&
                x.Date.Date == date.Date);
        }

        private bool IsExpiryValid(string expiry)
        {
            if (string.IsNullOrWhiteSpace(expiry) || expiry.Length != 5 || expiry[2] != '/')
                return false;

            if (!int.TryParse(expiry.Substring(0, 2), out int month) || month < 1 || month > 12)
                return false;

            if (!int.TryParse(expiry.Substring(3, 2), out int year))
                return false;

            year += 2000;
            var expiryDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            return expiryDate >= DateTime.Today;
        }

        private bool IsCreditCardValid(string card)
        {
            return !string.IsNullOrWhiteSpace(card) && card.Length == 16 && card.All(char.IsDigit);
        }

        private bool IsCvvValid(string cvv)
        {
            return !string.IsNullOrWhiteSpace(cvv) && cvv.Length == 3 && cvv.All(char.IsDigit);
        }

        private void DigitsOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit);
        }
    }
}
