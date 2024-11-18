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
    public partial class pageFazenda: Page
    {
        private FazendaViewModel _viewModel;

        public pageFazenda()
        {
            InitializeComponent();
            _viewModel = new FazendaViewModel();
            DataContext = _viewModel;
        }

        private async void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Fazenda.Clear();
            await _viewModel.CarregarFazendaDaAPI();
        }

        private void btnAddFazenda_Click(object sender, RoutedEventArgs e)
        {
                var janelaCadastro = new cadFazenda
                {
                    Owner = Window.GetWindow(this)
                };

                var viewModel = new FazendaCadastroViewModel(janelaCadastro.Close, null);
                janelaCadastro.DataContext = viewModel;
                janelaCadastro.ShowDialog();
        }

        private void FazendaDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Verifica se algum cliente está selecionado
            if (FazendaDataGrid.SelectedItem is Fazenda fazendaSelecionado)
            {
                // Cria o ViewModel para a janela de edição, passando o cliente selecionado
                var viewModel = new FazendaCadastroViewModel(null, fazendaSelecionado);

                // Cria a janela de edição
                var janelaCadastro = new cadFazenda
                {
                    DataContext = viewModel,
                    Owner = Window.GetWindow(this)  // Define o dono como a janela principal (MainWindow)
                };

                janelaCadastro.ShowDialog();  // Mostra a janela de edição modal (abre por cima da MainWindow)
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (FazendaDataGrid.SelectedItem is Fazenda fazendaSelecionado)
            {
                var viewModel = new FazendaCadastroViewModel(null, fazendaSelecionado);
                var janelaCadastro = new cadFazenda
                {
                    DataContext = viewModel,
                    Owner = Window.GetWindow(this)
                };

                janelaCadastro.ShowDialog(); 
            }
        }
    }
}
