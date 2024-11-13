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
    public partial class pageVenda : Page
    {
        private VendaViewModel _viewModel;

        public pageVenda()
        {
            InitializeComponent();
            _viewModel = new VendaViewModel();
            DataContext = _viewModel;
        }

        private async void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Venda.Clear();
            await _viewModel.CarregarVendaDaAPI();
        }

        private void btnAddVenda_Click(object sender, RoutedEventArgs e)
        {
                var janelaCadastro = new cadVenda
                {
                    Owner = Window.GetWindow(this)
                };

                var viewModel = new VendaCadastroViewModel(janelaCadastro.Close, null);
                janelaCadastro.DataContext = viewModel;
                janelaCadastro.ShowDialog();
        }

        private void VendaDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Verifica se algum cliente está selecionado
            if (VendaDataGrid.SelectedItem is Venda vendaSelecionado)
            {
                // Cria o ViewModel para a janela de edição, passando o cliente selecionado
                var viewModel = new VendaCadastroViewModel(null, vendaSelecionado);

                // Cria a janela de edição
                var janelaCadastro = new cadVenda
                {
                    DataContext = viewModel,
                    Owner = Window.GetWindow(this)  // Define o dono como a janela principal (MainWindow)
                };

                janelaCadastro.ShowDialog();  // Mostra a janela de edição modal (abre por cima da MainWindow)
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (VendaDataGrid.SelectedItem is Venda vendaSelecionado)
            {
                var viewModel = new VendaCadastroViewModel(null, vendaSelecionado);
                var janelaCadastro = new cadVenda
                {
                    DataContext = viewModel,
                    Owner = Window.GetWindow(this)
                };

                janelaCadastro.ShowDialog(); 
            }
        }
    }
}
