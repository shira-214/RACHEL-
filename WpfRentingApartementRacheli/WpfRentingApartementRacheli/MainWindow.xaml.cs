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

namespace WpfRentingApartementRacheli
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MyFrame.Navigate(new pageLogin());
        }

        private void worder_Click(object sender, RoutedEventArgs e)
        {
            ManagerPage porr = new ManagerPage();
            MyFrame.Navigate(porr);

        }

        private void wadd_Click(object sender, RoutedEventArgs e)
        {
            AddW padd = new AddW();
            MyFrame.Navigate(padd);


        }

        private void wsearch_Click(object sender, RoutedEventArgs e)
        {
            SearchW psrc = new SearchW();
            MyFrame.Navigate(psrc);

        }

        private void wmain_Click(object sender, RoutedEventArgs e)
        {
            MainWindow pmaim = new MainWindow();
            MyFrame.Navigate(pmaim);


        }


        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SearchW psrc = new SearchW();
            MyFrame.Navigate(psrc);

        }
    }
}
