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
    public partial class pageAreaPlantio : Page
    {
        private AreaPlantioViewModel _viewModel;

        public pageAreaPlantio()
        {
            InitializeComponent();
            _viewModel = new AreaPlantioViewModel();
            DataContext = _viewModel;
        }

        private async void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AreaPlantio.Clear();
            await _viewModel.CarregarAreaPlantioDaAPI();
        }

        private void btnAddAreaPlantio_Click(object sender, RoutedEventArgs e)
        {
                var janelaCadastro = new cadAreaPlantio
                {
                    Owner = Window.GetWindow(this)
                };

                var viewModel = new AreaPlantioCadastroViewModel(janelaCadastro.Close, null);
                janelaCadastro.DataContext = viewModel;
                janelaCadastro.ShowDialog();
        }

        private void AreaPlantioDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Verifica se algum cliente está selecionado
            if (AreaPlantioDataGrid.SelectedItem is AreaPlantio areaPlantioSelecionado)
            {
                // Cria o ViewModel para a janela de edição, passando o cliente selecionado
                var viewModel = new AreaPlantioCadastroViewModel(null, areaPlantioSelecionado);

                // Cria a janela de edição
                var janelaCadastro = new cadAreaPlantio
                {
                    DataContext = viewModel,
                    Owner = Window.GetWindow(this)  // Define o dono como a janela principal (MainWindow)
                };

                janelaCadastro.ShowDialog();  // Mostra a janela de edição modal (abre por cima da MainWindow)
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (AreaPlantioDataGrid.SelectedItem is AreaPlantio areaPlantioSelecionado)
            {
                var viewModel = new AreaPlantioCadastroViewModel(null, areaPlantioSelecionado);
                var janelaCadastro = new cadAreaPlantio
                {
                    DataContext = viewModel,
                    Owner = Window.GetWindow(this)
                };

                janelaCadastro.ShowDialog(); 
            }
        }
    }
}
