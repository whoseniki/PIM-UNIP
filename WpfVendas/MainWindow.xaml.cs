﻿using System.Text;
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
        private void btnRecurso_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new pageRecursos();
        }
        private void btnAreaPlantio_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new pageAreaPlantio();
        }
        private void btnPlantio_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new pagePlantio();
        }
        private void btnFuncionario_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new pageFuncionario();
        }
        private void btnVenda_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new pageVenda();
        }
        private void btnFazenda_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new pageFazenda();
        }
        private void btnManutencao_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new pageManutencao();
        }
        private void btnCompra_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new pageCompra();
        }
        private void btnSair_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();

            loginWindow.Show();

            this.Close();
        }
    }
}