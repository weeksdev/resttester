using MahApps.Metro.Controls;

namespace RestTester
{
    /// <summary>
    /// Interaction logic for HeadersWindow.xaml
    /// </summary>
    public partial class HeadersWindow : MetroWindow
    {
        public HeadersWindow(ViewModel.MainViewModel vm)
        {
            this.DataContext = vm;
            InitializeComponent();
        }
    }
}
