using DsiVendas.Models;
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
using WpfVendas.ViewModels;
using WpfVendas.Views;

namespace WpfVendas.Pages
{
    /// <summary>
    /// Interação lógica para pageClientes.xam
    /// </summary>
    public partial class pageCompra : Page
    {
        private CompraViewModel _viewModel;

        public pageCompra()
        {
            InitializeComponent();
            _viewModel = new CompraViewModel();
            DataContext = _viewModel;
        }

        private async void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Compra.Clear();
            await _viewModel.CarregarCompraDaAPI();
        }

        private void btnAddCompra_Click(object sender, RoutedEventArgs e)
        {
            var janelaCadastro = new cadCompra
            {
                Owner = Window.GetWindow(this)
            };

            var viewModel = new CompraCadastroViewModel(janelaCadastro.Close, null);
            janelaCadastro.DataContext = viewModel;
            janelaCadastro.ShowDialog();
        }

        private void CompraDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Verifica se algum cliente está selecionado
            if (CompraDataGrid.SelectedItem is Compra compraSelecionado)
            {
                // Cria o ViewModel para a janela de edição, passando o cliente selecionado
                var viewModel = new CompraCadastroViewModel(null, compraSelecionado);

                // Cria a janela de edição
                var janelaCadastro = new cadCompra
                {
                    DataContext = viewModel,
                    Owner = Window.GetWindow(this)  // Define o dono como a janela principal (MainWindow)
                };

                janelaCadastro.ShowDialog();  // Mostra a janela de edição modal (abre por cima da MainWindow)
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (CompraDataGrid.SelectedItem is Compra compraSelecionado)
            {
                var viewModel = new CompraCadastroViewModel(null, compraSelecionado);
                var janelaCadastro = new cadCompra
                {
                    DataContext = viewModel,
                    Owner = Window.GetWindow(this)
                };

                janelaCadastro.ShowDialog();
            }
        }
    }
}
