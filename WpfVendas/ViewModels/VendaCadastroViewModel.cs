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
    internal class VendaCadastroViewModel : INotifyPropertyChanged
    {
        private readonly HttpClient _httpClient;
        private Venda _venda;

        public Venda venda
        {
            get => _venda;
            set
            {
                _venda = value;
                OnPropertyChanged();
            }
        }

        public ICommand SalvarCommand { get; }
        public ICommand CancelarCommand { get; }

        private readonly Action _fecharAction;

        public VendaCadastroViewModel(Action fecharAction, Venda venda = null)
        {
            _fecharAction = fecharAction;
            Venda = venda ?? new Venda(); // Se o cliente for null, criamos um novo.

            // Inicializa o HttpClient (aqui ou injetado)
            _httpClient = new HttpClient();

            SalvarCommand = new RelayCommand(SalvarVenda);
            CancelarCommand = new RelayCommand(Cancelar);
        }

        private async void SalvarVenda(object obj)
        {
            if (Venda != null)
            {
                if (Venda.Id == 0)
                {
                    await CriarVendaAsync(Venda);
                }
                else
                {
                    await AtualizarVendaAsync(Venda);
                }

                _fecharAction(); // Fecha a janela após salvar
            }
        }

        private async Task CriarVendaAsync(Venda venda)
        {
            try
            {
                var apiUrl = "http://localhost:5299/Api/CreateVenda";
                var response = await _httpClient.PostAsJsonAsync(apiUrl, venda);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"´Venda {venda.Id} criado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Erro ao criar plantio: {response.StatusCode}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao criar plantio: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task AtualizarVendaAsync(Venda venda)
        {
            try
            {
                var apiUrl = $"http://localhost:5299/Api/UpdateVenda/{venda.Id}";
                var response = await _httpClient.PutAsJsonAsync(apiUrl, venda);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Venda {venda.Id} atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Erro ao atualizar venda: {response.StatusCode}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar venda: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
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
