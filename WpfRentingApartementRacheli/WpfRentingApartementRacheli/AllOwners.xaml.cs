using System.Linq;
using System.Windows.Controls;
using WpfRentingApartementRacheli.ServiceReference1;

namespace WpfRentingApartementRacheli
{
    public partial class AllOwners : Page
    {
        Service1Client server = new Service1Client();

        public AllOwners()
        {
            InitializeComponent();
            LoadOwners();
        }

        private void LoadOwners()
        {
            lvOwners.ItemsSource = server.GetApartments()
                .GroupBy(a => new { a.NameOwner, a.PhoneOwner })
                .Select(g => new OwnerView
                {
                    NameOwner = g.Key.NameOwner,
                    PhoneOwner = g.Key.PhoneOwner,
                    ApartmentCount = g.Count()
                })
                .OrderBy(o => o.NameOwner)
                .ToList();
        }
    }

    public class OwnerView
    {
        public string NameOwner { get; set; }
        public string PhoneOwner { get; set; }
        public int ApartmentCount { get; set; }
    }
}
