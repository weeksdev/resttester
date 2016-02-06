using System;
using System.Windows;
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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog dlg = new System.Windows.Forms.SaveFileDialog();
            dlg.Filter = "rest|*.rest";
            dlg.FileName = "request.rest";
            var result = dlg.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                var filename = dlg.FileName;
                vm.FilePath = filename;
                vm.Save();
            }
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog();
            dlg.Filter = "rest|*.rest";
            var result = dlg.ShowDialog();
            if(result == System.Windows.Forms.DialogResult.OK)
            {
                vm.FilePath = dlg.FileName;
                vm.Open();
            }
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
