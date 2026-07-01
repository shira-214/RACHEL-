using System;
using System.Collections.Generic;
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

            if (Global.currentHirers == null)
            {
                MessageBox.Show("יש להתחבר כשוכר כדי להזמין", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                NavigationService?.Navigate(new pageLogin());
                return;
            }

            this.apartment = apartment;
            sumPay = apartment.MinimumPrice;
            tbSumToPay.Text = sumPay.ToString();
            txtNumBeds.Tag = "מקסימום " + apartment.NumberBeds + " מיטות";

            string city = apartment.IdCities?.NameCity ?? "";
            string street = apartment.IdStreet?.StreetName ?? "";
            tbApartmentInfo.Text = city + " - " + street + " " + apartment.NumberHouse;

            LoadExtras();
            SetupDatePickerBlackouts();

            if (Global.selectedDate.Year != 1 && IsValidRentDate(Global.selectedDate.Date))
                dp.SelectedDate = Global.selectedDate.Date;
        }

        private void LoadExtras()
        {
            var extras = service.GetTOExtrasApartements()
                .Where(x => x.Status &&
                            x.IdAapartment != null &&
                            x.IdAapartment.IdApartment == apartment.IdApartment &&
                            x.IdExtra != null)
                .Select(x => x.IdExtra.NameExtra)
                .ToList();

            if (extras.Count == 0)
                lbExtras.ItemsSource = new List<string> { "אין תוספות רשומות" };
            else
                lbExtras.ItemsSource = extras;
        }

        private void SetupDatePickerBlackouts()
        {
            dp.BlackoutDates.Clear();

            DateTime today = DateTime.Today;
            if (today > DateTime.MinValue)
                dp.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, today.AddDays(-1)));

            var bookedDates = service.GetTORentings()
                .Where(x => x.KodHapartment != null && x.KodHapartment.IdApartment == apartment.IdApartment)
                .Select(x => x.Date.Date)
                .ToList();

            DateTime end = today.AddYears(2);
            for (DateTime date = today; date <= end; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday || bookedDates.Contains(date))
                    dp.BlackoutDates.Add(new CalendarDateRange(date));
            }
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

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            if (Global.currentHirers == null)
            {
                MessageBox.Show("לא התחברת", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                NavigationService?.Navigate(new pageLogin());
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

            DateTime rentDate = dp.SelectedDate.Value.Date;
            if (!IsValidRentDate(rentDate))
            {
                MessageBox.Show("ניתן לבחור רק תאריך שבת פנוי", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            UpdateSumPayment(beds);
            NavigationService?.Navigate(new pagePayment(apartment, beds, rentDate, sumPay));
        }

        private void txtNumBeds_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (int.TryParse(txtNumBeds.Text, out int beds) && beds > 0)
            {
                if (beds > apartment.NumberBeds)
                {
                    beds = apartment.NumberBeds;
                    txtNumBeds.Text = beds.ToString();
                    txtNumBeds.CaretIndex = txtNumBeds.Text.Length;
                    MessageBox.Show($"מספר המיטות לא יכול לעלות על {apartment.NumberBeds}",
                        "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                UpdateSumPayment(beds);
            }
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

        private void DigitsOnly_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!e.Text.All(char.IsDigit))
            {
                e.Handled = true;
                return;
            }

            if (sender is TextBox box)
            {
                string newText = box.Text.Remove(box.SelectionStart, box.SelectionLength)
                    .Insert(box.SelectionStart, e.Text);
                if (int.TryParse(newText, out int value) && value > apartment.NumberBeds)
                    e.Handled = true;
            }
        }
    }
}
