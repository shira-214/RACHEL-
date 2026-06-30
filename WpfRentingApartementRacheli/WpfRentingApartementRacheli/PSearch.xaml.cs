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
using WpfRentingApartementRacheli.USC;



namespace WpfRentingApartementRacheli
{
    /// <summary>
    /// Interaction logic for SearchW.xaml
    /// </summary>
    public partial class SearchW : Page
    {
        // יצירת מופע של השרת
        Service1Client service = new Service1Client();
        List<DTOApartments> lApartments;
        public SearchW()
        {
            InitializeComponent();

            ApartmentCities.ItemsSource = service.GetTOCities();
            ApartmentStreetsNames.ItemsSource = service.GetStreetsNames(); // הוספתי סוגריים
            lApartments = service.GetApartments().ToList();
            List<DTOApartments> lApartment = service.GetApartments().ToList();
            foreach (var item in lApartment)
                    {
                USCApartement uc = new USCApartement(item);
                wrp.Children.Add(uc);
            }
            RefreshData();
        }

        // האירוע שקישרנו ב-XAML לכל הפקדים
        private void Filter_Changed(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }

        private void RefreshData()
        {
            // כרגע לא פעיל, נשאר כפי שהיה
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Global.selectedDate = dp.SelectedDate.Value;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}