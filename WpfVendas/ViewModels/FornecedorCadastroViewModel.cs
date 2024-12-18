﻿using DsiVendas.Models;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace WpfVendas.ViewModels
{
    internal class FornecedorCadastroViewModel : INotifyPropertyChanged
    {

        private readonly HttpClient _httpClient;
        private Fornecedor _fornecedor;
        public Fornecedor Fornecedor
        {
            get => _fornecedor;
            set
            {
                _fornecedor = value;
                OnPropertyChanged();
            }
        }

        public ICommand SalvarCommand { get; }
        public ICommand CancelarCommand { get; }

        private readonly Action _fecharAction;

        public FornecedorCadastroViewModel(Action fecharAction, Fornecedor fornecedor = null)
        {
            _fecharAction = fecharAction;
            Fornecedor = fornecedor ?? new Fornecedor();

            _httpClient = new HttpClient();

            SalvarCommand = new RelayCommand(SalvarFornecedor);
            CancelarCommand = new RelayCommand(Cancelar);
        }

        private async void SalvarFornecedor(object obj)
        {
            if (Fornecedor != null)
            {
                if (Fornecedor.Id == 0)
                {
                    await CriarFornecedorAsync(Fornecedor);
                }
                else
                {
                    await AtualizarFornecedorAsync(Fornecedor);
                }

                _fecharAction(); // Fecha a janela após salvar
            }
        }

        private async Task CriarFornecedorAsync(Fornecedor fornecedor)
        {
            try
            {
                var apiUrl = "http://localhost:5299/Api/CreateFornecedor";
                var response = await _httpClient.PostAsJsonAsync(apiUrl, fornecedor);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Fornecedor {fornecedor.RazaoSocial} criado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Erro ao criar o fornecedor: {response.StatusCode}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao criar o fornecedor: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task AtualizarFornecedorAsync(Fornecedor fornecedor)
        {
            try
            {
                var apiUrl = $"http://localhost:3000/Api/UpdateFornecedor/{fornecedor.Id}";
                var response = await _httpClient.PutAsJsonAsync(apiUrl, fornecedor);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Fornecedor {fornecedor.RazaoSocial} atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                         }
                else
                {
                    MessageBox.Show($"Erro ao salvar o fornecedor: {response.StatusCode}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                // Captura exceções e mostra a mensagem de erro
                Console.WriteLine($"Erro ao atualizar o fornecedor: {ex.Message}");
            }
        }

        private void Cancelar(object obj)
        {
            _fecharAction(); // Fecha a janela sem salvar
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
