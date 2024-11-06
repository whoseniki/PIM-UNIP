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
    public partial class pageProdutos : Page
    {
        private ProdutoViewModel _viewModel;

        public pageProdutos()
        {
            InitializeComponent();
            _viewModel = new ProdutoViewModel();
            DataContext = _viewModel;
        }

        private async void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Produto.Clear();
            await _viewModel.CarregarProdutoDaAPI();
        }

        private void btnAddProduto_Click(object sender, RoutedEventArgs e)
        {
            cadProduto cadProduto = new cadProduto();
            cadProduto.ShowDialog();
        }

        private void ProdutoDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Verifica se algum cliente está selecionado
            if (ProdutoDataGrid.SelectedItem is Produto produtoSelecionado)
            {
                // Cria o ViewModel para a janela de edição, passando o cliente selecionado
                var viewModel = new ProdutoCadastroViewModel(null, produtoSelecionado);

                // Cria a janela de edição
                var janelaCadastro = new cadProduto
                {
                    DataContext = viewModel,
                    Owner = Window.GetWindow(this)  // Define o dono como a janela principal (MainWindow)
                };

                janelaCadastro.ShowDialog();  // Mostra a janela de edição modal (abre por cima da MainWindow)
            }
        }
    }
}
