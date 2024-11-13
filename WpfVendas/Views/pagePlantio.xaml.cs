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
    public partial class pagePlantio : Page
    {
        private PlantioViewModel _viewModel;

        public pagePlantio()
        {
            InitializeComponent();
            _viewModel = new PlantioViewModel();
            DataContext = _viewModel;
        }

        private async void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Plantio.Clear();
            await _viewModel.CarregarPlantioDaAPI();
        }

        private void btnAddPlantio_Click(object sender, RoutedEventArgs e)
        {
                var janelaCadastro = new cadPlantio
                {
                    Owner = Window.GetWindow(this)
                };

                var viewModel = new PlantioCadastroViewModel(janelaCadastro.Close, null);
                janelaCadastro.DataContext = viewModel;
                janelaCadastro.ShowDialog();
        }

        private void PlantioDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Verifica se algum cliente está selecionado
            if (PlantioDataGrid.SelectedItem is Plantio plantioSelecionado)
            {
                // Cria o ViewModel para a janela de edição, passando o cliente selecionado
                var viewModel = new PlantioCadastroViewModel(null, plantioSelecionado);

                // Cria a janela de edição
                var janelaCadastro = new cadPlantio
                {
                    DataContext = viewModel,
                    Owner = Window.GetWindow(this)  // Define o dono como a janela principal (MainWindow)
                };

                janelaCadastro.ShowDialog();  // Mostra a janela de edição modal (abre por cima da MainWindow)
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (PlantioDataGrid.SelectedItem is Plantio plantioSelecionado)
            {
                var viewModel = new PlantioCadastroViewModel(null, plantioSelecionado);
                var janelaCadastro = new cadPlantio
                {
                    DataContext = viewModel,
                    Owner = Window.GetWindow(this)
                };

                janelaCadastro.ShowDialog(); 
            }
        }
    }
}
