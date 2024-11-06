using DsiVendas.Models;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfVendas.ViewModels
{
    internal class ClienteCadastroViewModel : INotifyPropertyChanged
    {
        private readonly HttpClient _httpClient;
        private Cliente _cliente;

        public Cliente Cliente
        {
            get => _cliente;
            set
            {
                _cliente = value;
                OnPropertyChanged();
            }
        }

        public ICommand SalvarCommand { get; }
        public ICommand CancelarCommand { get; }

        private readonly Action _fecharAction;

        public ClienteCadastroViewModel(Action fecharAction, Cliente cliente = null)
        {
            _fecharAction = fecharAction;
            Cliente = cliente ?? new Cliente(); // Se o cliente for null, criamos um novo.

            // Inicializa o HttpClient (aqui ou injetado)
            _httpClient = new HttpClient();

            SalvarCommand = new RelayCommand(SalvarCliente);
            CancelarCommand = new RelayCommand(Cancelar);
        }

        private async void SalvarCliente(object obj)
        {
            if (Cliente != null)
            {
                if (Cliente.Id == 0)
                {
                    await CriarClienteAsync(Cliente);
                }
                else
                {
                    await AtualizarClienteAsync(Cliente);
                }

                _fecharAction(); // Fecha a janela após salvar
            }
        }

        private async Task CriarClienteAsync(Cliente cliente)
        {
            try
            {
                var apiUrl = "http://localhost:5299/Api/CreateCliente";
                var response = await _httpClient.PostAsJsonAsync(apiUrl, cliente);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Cliente {cliente.Nome} criado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Erro ao criar o cliente: {response.StatusCode}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao criar o cliente: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task AtualizarClienteAsync(Cliente cliente)
        {
            try
            {
                var apiUrl = $"http://localhost:5299/Api/UpdateCliente/{cliente.Id}";
                var response = await _httpClient.PutAsJsonAsync(apiUrl, cliente);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Cliente {cliente.Nome} atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Erro ao atualizar o cliente: {response.StatusCode}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar o cliente: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
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
