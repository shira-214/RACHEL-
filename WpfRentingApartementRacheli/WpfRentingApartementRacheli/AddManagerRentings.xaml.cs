using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfRentingApartementRacheli.ServiceReference1;

namespace WpfRentingApartementRacheli
{
    public partial class AddManagerRentings : Page
    {
        Service1Client server = new Service1Client();
        DTORentings renting;
        bool add;

        public AddManagerRentings()
        {
            InitializeComponent();
            renting = new DTORentings();
            add = true;
            LoadCombos();
        }

        public AddManagerRentings(DTORentings existing)
        {
            InitializeComponent();
            renting = existing;
            add = false;
            LoadCombos();

            if (renting.IdHirer != null)
                cmbHirer.SelectedItem = server.GetTOHirers()
                    .FirstOrDefault(h => h.C_IDHirer == renting.IdHirer.C_IDHirer);
            if (renting.KodHapartment != null)
                cmbApartment.SelectedItem = server.GetApartments()
                    .FirstOrDefault(a => a.IdApartment == renting.KodHapartment.IdApartment);
            if (renting.Date.Year > 1)
                dpDate.SelectedDate = renting.Date;
            txtSumBeds.Text = renting.SumBeds.ToString();
            tbSumPayment.Text = renting.SumPayment.ToString();
        }

        private void LoadCombos()
        {
            cmbHirer.ItemsSource = server.GetTOHirers();
            cmbApartment.ItemsSource = server.GetApartments();
        }

        private void cmbApartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateSumPayment();
        }

        private void txtSumBeds_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSumPayment();
        }

        private void UpdateSumPayment()
        {
            if (cmbApartment.SelectedItem is DTOApartments apt &&
                int.TryParse(txtSumBeds.Text, out int beds) && beds > 0)
            {
                int sum = apt.MinimumPrice + apt.ExtraForBed * beds;
                tbSumPayment.Text = sum.ToString();
            }
            else
                tbSumPayment.Text = "";
        }

        private void btnEnd_Click(object sender, RoutedEventArgs e)
        {
            if (cmbHirer.SelectedItem == null || cmbApartment.SelectedItem == null ||
                !dpDate.SelectedDate.HasValue ||
                !int.TryParse(txtSumBeds.Text, out int beds) || beds <= 0)
            {
                MessageBox.Show("יש למלא את כל השדות", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var apartment = (DTOApartments)cmbApartment.SelectedItem;
            if (beds > apartment.NumberBeds)
            {
                MessageBox.Show("מספר המיטות גדול מהקיבולת בדירה", "שגיאה",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            DateTime rentDate = dpDate.SelectedDate.Value.Date;
            bool conflict = server.GetTORentings().Any(r =>
                r.KodHapartment != null &&
                r.KodHapartment.IdApartment == apartment.IdApartment &&
                r.Date.Date == rentDate &&
                (add || r.IdRenting != renting.IdRenting));

            if (conflict)
            {
                MessageBox.Show("הדירה תפוסה בתאריך זה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            renting.IdHirer = (DTOHirers)cmbHirer.SelectedItem;
            renting.KodHapartment = apartment;
            renting.Date = rentDate;
            renting.SumBeds = beds;
            renting.SumPayment = apartment.MinimumPrice + apartment.ExtraForBed * beds;

            bool ok;
            if (add)
                ok = server.Addentings(renting);
            else
                ok = server.UpdateRentings(renting);

            if (ok)
            {
                MessageBox.Show(add ? "נוסף בהצלחה!" : "עודכן בהצלחה!", "הצלחה",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService?.Navigate(new AllRentings());
            }
            else
                MessageBox.Show("שגיאה בשמירה", "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
