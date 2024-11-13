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
    internal class AreaPlantioCadastroViewModel : INotifyPropertyChanged
    {
        private readonly HttpClient _httpClient;
        private AreaPlantio _areaPlantio;

        public AreaPlantio AreaPlantio
        {
            get => _areaPlantio;
            set
            {
                _areaPlantio = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _dataPlantio;
        public DateTime? DataPlantio

        {
            get => _dataPlantio;
            set
            {
                if (_dataPlantio != value)
                {
                    _dataPlantio = value;
                    OnPropertyChanged(nameof(DataPlantio));
                }
            }
        }

        public ICommand SalvarCommand { get; }
        public ICommand CancelarCommand { get; }

        private readonly Action _fecharAction;

        public AreaPlantioCadastroViewModel(Action fecharAction, AreaPlantio areaPlantio = null)
        {
            _fecharAction = fecharAction;
            AreaPlantio = areaPlantio ?? new AreaPlantio(); // Se o cliente for null, criamos um novo.

            // Inicializa o HttpClient (aqui ou injetado)
            _httpClient = new HttpClient();

            SalvarCommand = new RelayCommand(SalvarAreaPlantio);
            CancelarCommand = new RelayCommand(Cancelar);
        }

        private async void SalvarAreaPlantio(object obj)
        {
            if (AreaPlantio != null)
            {
                if (AreaPlantio.Id == 0)
                {
                    await CriarAreaPlantioAsync(AreaPlantio);
                }
                else
                {
                    await AtualizarAreaPlantioAsync(AreaPlantio);
                }

                _fecharAction(); // Fecha a janela após salvar
            }
        }

        private async Task CriarAreaPlantioAsync(AreaPlantio areaPlantio)
        {
            try
            {
                var apiUrl = "http://localhost:5299/Api/CreateAreaPlantio";
                var response = await _httpClient.PostAsJsonAsync(apiUrl, areaPlantio);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Área de plantio {areaPlantio.Nome} criado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Erro ao criar a área de plantio: {response.StatusCode}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao criar a área de plantio: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task AtualizarAreaPlantioAsync(AreaPlantio areaPlantio)
        {
            try
            {
                var apiUrl = $"http://localhost:5299/Api/UpdateAreaPlantio/{areaPlantio.Id}";
                var response = await _httpClient.PutAsJsonAsync(apiUrl, areaPlantio);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Área de plantio {areaPlantio.Nome} atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Erro ao atualizar a área de plantio: {response.StatusCode}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar a área de plantio: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
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
