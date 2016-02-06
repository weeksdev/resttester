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
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace RestTester
{
    /// <summary>
    /// Interaction logic for CredentialsWindow.xaml
    /// </summary>
    public partial class CredentialsWindow : MetroWindow
    {
        public CredentialsWindow(ViewModel.MainViewModel vm)
        {
            this.DataContext = vm;
            InitializeComponent();
        }
    }
}
