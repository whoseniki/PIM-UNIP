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
    internal class ManutencaoCadastroViewModel : INotifyPropertyChanged
    {
        private readonly HttpClient _httpClient;
        private Manutencao _manutencao;

        public Manutencao Manutencao
        {
            get => _manutencao;
            set
            {
                _manutencao = value;
                OnPropertyChanged();
            }
        }

        public ICommand SalvarCommand { get; }
        public ICommand CancelarCommand { get; }

        private readonly Action _fecharAction;

        public ManutencaoCadastroViewModel(Action fecharAction, Manutencao manutencao = null)
        {
            _fecharAction = fecharAction;
            Manutencao = manutencao ?? new Manutencao(); // Se o cliente for null, criamos um novo.

            // Inicializa o HttpClient (aqui ou injetado)
            _httpClient = new HttpClient();

            SalvarCommand = new RelayCommand(SalvarManutencao);
            CancelarCommand = new RelayCommand(Cancelar);
        }

        private async void SalvarManutencao(object obj)
        {
            if (Manutencao != null)
            {
                if (Manutencao.Id == 0)
                {
                    await CriarManutencaoAsync(Manutencao);
                }
                else
                {
                    await AtualizarManutencaoAsync(Manutencao);
                }

                _fecharAction(); // Fecha a janela após salvar
            }
        }

        private async Task CriarManutencaoAsync(Manutencao manutencao)
        {
            try
            {
                var apiUrl = "http://localhost:5299/Api/CreateManutencao";
                var response = await _httpClient.PostAsJsonAsync(apiUrl, manutencao);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"´Manutencao {manutencao.Id} criado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Erro ao criar manutencao: {response.StatusCode}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao criar manutencao: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task AtualizarManutencaoAsync(Manutencao manutencao)
        {
            try
            {
                var apiUrl = $"http://localhost:5299/Api/UpdateManutencao/{manutencao.Id}";
                var response = await _httpClient.PutAsJsonAsync(apiUrl, manutencao);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Manutencao {manutencao.Id} atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Erro ao atualizar manutencao: {response.StatusCode}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar manutencao: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
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
