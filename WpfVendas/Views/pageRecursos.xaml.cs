﻿using DsiVendas.Models;
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
    public partial class pageRecursos : Page
    {
        private RecursoViewModel _viewModel;

        public pageRecursos()
        {
            InitializeComponent();
            _viewModel = new RecursoViewModel();
            DataContext = _viewModel;
        }

        private async void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Recurso.Clear();
            await _viewModel.CarregarRecursoDaAPI();
        }

        private void btnAddRecurso_Click(object sender, RoutedEventArgs e)
        {
            var janelaCadastro = new cadRecurso
            {
                Owner = Window.GetWindow(this)
            };

            var viewModel = new RecursoCadastroViewModel(janelaCadastro.Close, null);
            janelaCadastro.DataContext = viewModel;
            janelaCadastro.ShowDialog();
        }

        private void RecursoDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Verifica se algum cliente está selecionado
            if (RecursoDataGrid.SelectedItem is Recurso recursoSelecionado)
            {
                // Cria o ViewModel para a janela de edição, passando o cliente selecionado
                var viewModel = new RecursoCadastroViewModel(null, recursoSelecionado);

                // Cria a janela de edição
                var janelaCadastro = new cadRecurso
                {
                    DataContext = viewModel,
                    Owner = Window.GetWindow(this)  // Define o dono como a janela principal (MainWindow)
                };

                janelaCadastro.ShowDialog();  // Mostra a janela de edição modal (abre por cima da MainWindow)
            }
        }
    }
}
