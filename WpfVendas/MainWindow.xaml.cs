using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfVendas.Pages;

namespace WpfVendas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnClientes_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new pageClientes();
        }

        private void btnFornecedores_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new pageFornecedores();
        }
        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}