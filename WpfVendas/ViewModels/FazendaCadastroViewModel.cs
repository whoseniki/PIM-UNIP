using DsiVendas.Models;
using System;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace WpfVendas.ViewModels
{
    internal class FazendaCadastroViewModel : INotifyPropertyChanged
    {

        private readonly HttpClient _httpClient;
        private Fazenda _fazenda;
        public Fazenda Fazenda
        {
            get => _fazenda;
            set
            {
                _fazenda = value;
                OnPropertyChanged();
            }
        }

        public ICommand SalvarCommand { get; }
        public ICommand CancelarCommand { get; }

        private readonly Action _fecharAction;

        public FazendaCadastroViewModel(Action fecharAction, Fazenda fazenda = null)
        {
            _fecharAction = fecharAction;
            Fazenda = fazenda ?? new Fazenda();

            _httpClient = new HttpClient();

            SalvarCommand = new RelayCommand(SalvarFazenda);
            CancelarCommand = new RelayCommand(Cancelar);
        }

        private async void SalvarFazenda(object obj)
        {
            if (Fazenda != null)
            {
                if (Fazenda.Id == 0)
                {
                    await CriarFazendaAsync(Fazenda);
                }
                else
                {
                    await AtualizarFazendaAsync(Fazenda);
                }

                _fecharAction(); // Fecha a janela após salvar
            }
        }

        private async Task CriarFazendaAsync(Fazenda fazenda)
        {
            try
            {
                var apiUrl = "http://localhost:5299/Api/CreateFazenda";
                var response = await _httpClient.PostAsJsonAsync(apiUrl, fazenda);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Fazenda {fazenda.Id} criado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Erro ao criar a fazenda: {response.StatusCode}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao criar a fazenda: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task AtualizarFazendaAsync(Fazenda fazenda)
        {
            try
            {
                var apiUrl = $"http://localhost:3000/Api/UpdateFazenda/{fazenda.Id}";
                var response = await _httpClient.PutAsJsonAsync(apiUrl, fazenda);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Fazenda {fazenda.Id} atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                         }
                else
                {
                    MessageBox.Show($"Erro ao salvar a fazenda: {response.StatusCode}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                // Captura exceções e mostra a mensagem de erro
                Console.WriteLine($"Erro ao atualizar a fazenda: {ex.Message}");
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
