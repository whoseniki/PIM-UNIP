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
    public partial class pageManutencao : Page
    {
        private ManutencaoViewModel _viewModel;

        public pageManutencao()
        {
            InitializeComponent();
            _viewModel = new ManutencaoViewModel();
            DataContext = _viewModel;
        }

        private async void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Manutencao.Clear();
            await _viewModel.CarregarManutencaoDaAPI();
        }

        private void btnAddManutencao_Click(object sender, RoutedEventArgs e)
        {
                var janelaCadastro = new cadManutencao
                {
                    Owner = Window.GetWindow(this)
                };

                var viewModel = new ManutencaoCadastroViewModel(janelaCadastro.Close, null);
                janelaCadastro.DataContext = viewModel;
                janelaCadastro.ShowDialog();
        }

        private void ManutencaoDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Verifica se algum cliente está selecionado
            if (ManutencaoDataGrid.SelectedItem is Manutencao manutencaoSelecionado)
            {
                // Cria o ViewModel para a janela de edição, passando o cliente selecionado
                var viewModel = new ManutencaoCadastroViewModel(null, manutencaoSelecionado);

                // Cria a janela de edição
                var janelaCadastro = new cadManutencao
                {
                    DataContext = viewModel,
                    Owner = Window.GetWindow(this)  // Define o dono como a janela principal (MainWindow)
                };

                janelaCadastro.ShowDialog();  // Mostra a janela de edição modal (abre por cima da MainWindow)
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (ManutencaoDataGrid.SelectedItem is Manutencao manutencaoSelecionado)
            {
                var viewModel = new ManutencaoCadastroViewModel(null, manutencaoSelecionado);
                var janelaCadastro = new cadManutencao
                {
                    DataContext = viewModel,
                    Owner = Window.GetWindow(this)
                };

                janelaCadastro.ShowDialog(); 
            }
        }
    }
}
