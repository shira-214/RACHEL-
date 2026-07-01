using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfRentingApartementRacheli.ServiceReference1;

namespace WpfRentingApartementRacheli
{
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
            tbSumToPay.Text = sumPay.ToString();

            string city = apartment.IdCities?.NameCity ?? "";
            string street = apartment.IdStreet?.StreetName ?? "";
            tbApartmentInfo.Text = city + " - " + street + " " + apartment.NumberHouse;

            if (Global.selectedDate.Year != 1)
                dp.SelectedDate = Global.selectedDate;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Global.currentHirers == null)
            {
                MessageBox.Show("לא התחברת", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(txtNumBeds.Text, out int beds) || beds <= 0)
            {
                MessageBox.Show("יש להזין מספר מיטות תקין", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (beds > apartment.NumberBeds)
            {
                MessageBox.Show($"מספר המיטות גדול מהקיבולת בדירה ({apartment.NumberBeds})",
                    "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!dp.SelectedDate.HasValue)
            {
                MessageBox.Show("לא נבחר תאריך", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!IsCreditCardValid(txtCard.Text) || !IsCvvValid(txtCvv.Text) || !IsExpiryValid(txtExpiry.Text))
            {
                MessageBox.Show("פרטי אשראי לא תקינים", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DateTime rentDate = dp.SelectedDate.Value.Date;
            if (service.GetTORentings().Any(x =>
                x.KodHapartment != null &&
                x.KodHapartment.IdApartment == apartment.IdApartment &&
                x.Date.Date == rentDate))
            {
                MessageBox.Show("הדירה תפוסה בתאריך זה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            UpdateSumPayment(beds);

            DTORentings renting = new DTORentings
            {
                IdHirer = Global.currentHirers,
                KodHapartment = apartment,
                Date = rentDate,
                SumPayment = sumPay,
                SumBeds = beds
            };

            service.Addentings(renting);
            MessageBox.Show("ההזמנה נוספה בהצלחה!", "הצלחה", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.Navigate(new SearchW());
        }

        private void txtNumBeds_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(txtNumBeds.Text, out int beds) && beds > 0)
                UpdateSumPayment(beds);
            else
            {
                sumPay = apartment.MinimumPrice;
                tbSumToPay.Text = sumPay.ToString();
            }
        }

        private void UpdateSumPayment(int beds)
        {
            sumPay = apartment.MinimumPrice + apartment.ExtraForBed * beds;
            tbSumToPay.Text = sumPay.ToString();
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