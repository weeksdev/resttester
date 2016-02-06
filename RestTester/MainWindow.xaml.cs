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
using MahApps.Metro.Controls;

namespace RestTester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        ViewModel.MainViewModel vm;
        ThemeWindow themeWindow;
        CredentialsWindow credWindow;
        HeadersWindow headersWindow;
        public MainWindow()
        {
            vm = new ViewModel.MainViewModel();
            this.DataContext = vm;
            InitializeComponent();
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            
            Application.Current.Shutdown();
        }
        private void ThemeButton_Click(object sender, RoutedEventArgs e)
        {
            if (themeWindow != null)
            {
                themeWindow = null;
            }
            themeWindow = new ThemeWindow();
            themeWindow.Show();
        }

        private void CredentialsButton_Click(object sender, RoutedEventArgs e)
        {
            if(credWindow != null)
            {
                credWindow = null;
            }
            credWindow = new CredentialsWindow(vm);
            credWindow.Show();
        }

        private void HeadersButton_Click(object sender, RoutedEventArgs e)
        {
            if(headersWindow != null)
            {
                headersWindow = null;
            }
            headersWindow = new HeadersWindow(vm);
            headersWindow.Show();
        }
    }
}
