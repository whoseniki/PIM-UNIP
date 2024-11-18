using DsiVendas.Models;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace WpfVendas.ViewModels
{
    internal class CompraCadastroViewModel : INotifyPropertyChanged
    {
        private readonly HttpClient _httpClient;
        private Compra _compra;

        public Compra Compra
        {
            get => _compra;
            set
            {
                _compra = value;
                OnPropertyChanged();
            }
        }

        public ICommand SalvarCommand { get; }
        public ICommand CancelarCommand { get; }

        private readonly Action _fecharAction;

        public CompraCadastroViewModel(Action fecharAction, Compra compra = null)
        {
            _fecharAction = fecharAction;
            Compra = compra ?? new Compra(); // Se o cliente for null, criamos um novo.

            // Inicializa o HttpClient (aqui ou injetado)
            _httpClient = new HttpClient();

            SalvarCommand = new RelayCommand(SalvarCompra);
            CancelarCommand = new RelayCommand(Cancelar);
        }

        private async void SalvarCompra(object obj)
        {
            if (Compra != null)
            {
                if (Compra.Id == 0)
                {
                    await CriarCompraAsync(Compra);
                }
                else
                {
                    await AtualizarCompraAsync(Compra);
                }

                _fecharAction(); // Fecha a janela após salvar
            }
        }

        private async Task CriarCompraAsync(Compra compra)
        {
            try
            {
                var apiUrl = "http://localhost:5299/Api/CreateCompra";
                var response = await _httpClient.PostAsJsonAsync(apiUrl, compra);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"´Compra {compra.Id} criado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Erro ao criar compra: {response.StatusCode}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao criar compra: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task AtualizarCompraAsync(Compra compra)
        {
            try
            {
                var apiUrl = $"http://localhost:5299/Api/UpdateCompra/{compra.Id}";
                var response = await _httpClient.PutAsJsonAsync(apiUrl, compra);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Compra {compra.Id} atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Erro ao atualizar compra: {response.StatusCode}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar compra: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
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
