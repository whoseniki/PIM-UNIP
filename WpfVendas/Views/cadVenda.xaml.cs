﻿using System;
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
using System.Windows.Shapes;
using DsiVendas.Models;
using WpfVendas.ViewModels;

namespace WpfVendas.Views
{
    /// <summary>
    /// Lógica interna para cadCliente.xaml
    /// </summary>
    public partial class cadVenda : Window
    {
        public cadVenda()
        {
            InitializeComponent();
        }
        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            var janelaCadastro = new cadItemVenda
            {
                Owner = Window.GetWindow(this)
            };

            var viewModel = new ItemVendaCadastroViewModel(janelaCadastro.Close, null);
            janelaCadastro.DataContext = viewModel;
            janelaCadastro.ShowDialog();
        }
    }
}