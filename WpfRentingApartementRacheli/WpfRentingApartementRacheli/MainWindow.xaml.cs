using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfRentingApartementRacheli
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MyFrame.Navigated += MyFrame_Navigated;
            MyFrame.Navigate(new LandingPage());
            UpdateToolbar();
        }

        private void MyFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            if (e.Content is FrameworkElement element)
                element.FlowDirection = FlowDirection.RightToLeft;
            UpdateToolbar();
        }

        public void UpdateToolbar()
        {
            wmain.Visibility = Visibility.Collapsed;
            wsearch.Visibility = Visibility.Collapsed;
            btnMyRentings.Visibility = Visibility.Collapsed;
            wadd.Visibility = Visibility.Collapsed;
            btnOwnerDash.Visibility = Visibility.Collapsed;
            worder.Visibility = Visibility.Collapsed;
            btnLogout.Visibility = Visibility.Collapsed;

            if (Global.CurrentRole == Global.UserRole.None)
                return;

            wmain.Visibility = Visibility.Visible;
            btnLogout.Visibility = Visibility.Visible;

            if (Global.CurrentRole == Global.UserRole.Hirer)
            {
                wsearch.Visibility = Visibility.Visible;
                btnMyRentings.Visibility = Visibility.Visible;
            }
            else if (Global.CurrentRole == Global.UserRole.Owner)
            {
                wadd.Visibility = Visibility.Visible;
                btnOwnerDash.Visibility = Visibility.Visible;
            }
            else if (Global.CurrentRole == Global.UserRole.Manager)
            {
                worder.Visibility = Visibility.Visible;
            }
        }

        private void worder_Click(object sender, RoutedEventArgs e)
        {
            if (Global.CurrentRole == Global.UserRole.Manager)
                MyFrame.Navigate(new ManagerPage());
            else
                MyFrame.Navigate(new ManagerLoginPage());
        }

        private void wadd_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(new AddW());
        }

        private void wsearch_Click(object sender, RoutedEventArgs e)
        {
            if (Global.CurrentRole == Global.UserRole.Hirer)
                MyFrame.Navigate(new SearchW());
            else
                MyFrame.Navigate(new pageLogin());
        }

        private void btnMyRentings_Click(object sender, RoutedEventArgs e)
        {
            if (Global.CurrentRole == Global.UserRole.Hirer)
                MyFrame.Navigate(new MyRentingsPage());
            else
                MyFrame.Navigate(new pageLogin());
        }

        private void btnOwnerDash_Click(object sender, RoutedEventArgs e)
        {
            if (Global.CurrentRole == Global.UserRole.Owner)
                MyFrame.Navigate(new OwnerDashboardPage());
            else
                MyFrame.Navigate(new OwnerLoginPage());
        }

        private void wmain_Click(object sender, RoutedEventArgs e)
        {
            Global.Logout();
            MyFrame.Navigate(new LandingPage());
            UpdateToolbar();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Global.Logout();
            MyFrame.Navigate(new LandingPage());
            UpdateToolbar();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Global.CurrentRole == Global.UserRole.Hirer)
                MyFrame.Navigate(new SearchW());
            else if (Global.CurrentRole == Global.UserRole.Owner)
                MyFrame.Navigate(new OwnerDashboardPage());
            else if (Global.CurrentRole == Global.UserRole.Manager)
                MyFrame.Navigate(new ManagerPage());
            else
                MyFrame.Navigate(new LandingPage());
        }
    }
}
