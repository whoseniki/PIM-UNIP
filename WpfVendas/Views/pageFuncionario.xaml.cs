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
    public partial class pageFuncionario : Page
    {
        private FuncionarioViewModel _viewModel;

        public pageFuncionario()
        {
            InitializeComponent();
            _viewModel = new FuncionarioViewModel();
            DataContext = _viewModel;
        }

        private async void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Funcionario.Clear();
            await _viewModel.CarregarFuncionarioDaAPI();
        }

        private void btnAddFuncionario_Click(object sender, RoutedEventArgs e)
        {
            var janelaCadastro = new cadFuncionario
            {
                Owner = Window.GetWindow(this)
            };

            var viewModel = new FuncionarioCadastroViewModel(janelaCadastro.Close, null);
            janelaCadastro.DataContext = viewModel;
            janelaCadastro.ShowDialog();
        }

        private void FuncionarioDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Verifica se algum cliente está selecionado
            if (FuncionarioDataGrid.SelectedItem is Funcionario funcionarioSelecionado)
            {
                // Cria o ViewModel para a janela de edição, passando o cliente selecionado
                var viewModel = new FuncionarioCadastroViewModel(null, funcionarioSelecionado);

                // Cria a janela de edição
                var janelaCadastro = new cadFuncionario
                {
                    DataContext = viewModel,
                    Owner = Window.GetWindow(this)  // Define o dono como a janela principal (MainWindow)
                };

                janelaCadastro.ShowDialog();  // Mostra a janela de edição modal (abre por cima da MainWindow)
            }
        }
    }
}
