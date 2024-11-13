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
    internal class PlantioCadastroViewModel : INotifyPropertyChanged
    {
        private readonly HttpClient _httpClient;
        private Plantio _plantio;

        public Plantio Plantio
        {
            get => _plantio;
            set
            {
                _plantio = value;
                OnPropertyChanged();
            }
        }

        public ICommand SalvarCommand { get; }
        public ICommand CancelarCommand { get; }

        private readonly Action _fecharAction;

        public PlantioCadastroViewModel(Action fecharAction, Plantio plantio = null)
        {
            _fecharAction = fecharAction;
            Plantio = plantio ?? new Plantio(); // Se o cliente for null, criamos um novo.

            // Inicializa o HttpClient (aqui ou injetado)
            _httpClient = new HttpClient();

            SalvarCommand = new RelayCommand(SalvarPlantio);
            CancelarCommand = new RelayCommand(Cancelar);
        }

        private async void SalvarPlantio(object obj)
        {
            if (Plantio != null)
            {
                if (Plantio.Id == 0)
                {
                    await CriarPlantioAsync(Plantio);
                }
                else
                {
                    await AtualizarPlantioAsync(Plantio);
                }

                _fecharAction(); // Fecha a janela após salvar
            }
        }

        private async Task CriarPlantioAsync(Plantio plantio)
        {
            try
            {
                var apiUrl = "http://localhost:5299/Api/CreatePlantio";
                var response = await _httpClient.PostAsJsonAsync(apiUrl, plantio);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"´Plantio {plantio.Nome} criado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private async Task AtualizarPlantioAsync(Plantio plantio)
        {
            try
            {
                var apiUrl = $"http://localhost:5299/Api/UpdatePlantio/{plantio.Id}";
                var response = await _httpClient.PutAsJsonAsync(apiUrl, plantio);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Plantio {plantio.Nome} atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Erro ao atualizar plantio: {response.StatusCode}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar plantio: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
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
