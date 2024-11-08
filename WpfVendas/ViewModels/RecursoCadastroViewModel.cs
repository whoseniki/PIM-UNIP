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
    internal class RecursoCadastroViewModel : INotifyPropertyChanged
    {

        private readonly HttpClient _httpClient;
        private Recurso _recurso;
        public Recurso Recurso
        {
            get => _recurso;
            set
            {
                _recurso = value;
                OnPropertyChanged();
            }
        }

        public ICommand SalvarCommand { get; }
        public ICommand CancelarCommand { get; }

        private readonly Action _fecharAction;

        public RecursoCadastroViewModel(Action fecharAction, Recurso recurso = null)
        {
            _fecharAction = fecharAction;
            Recurso = recurso ?? new Recurso();

            _httpClient = new HttpClient();

            SalvarCommand = new RelayCommand(SalvarRecurso);
            CancelarCommand = new RelayCommand(Cancelar);
        }

        private async void SalvarRecurso(object obj)
        {
            if (Recurso != null)
            {
                if (Recurso.Id == 0)
                {
                    await CriarRecursoAsync(Recurso);
                }
                else
                {
                    await AtualizarRecursoAsync(Recurso);
                }

                _fecharAction(); // Fecha a janela após salvar
            }
        }

        private async Task CriarRecursoAsync(Recurso recurso)
        {
            try
            {
                var apiUrl = "http://localhost:5299/Api/CreateRecurso";
                var response = await _httpClient.PostAsJsonAsync(apiUrl, recurso);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Recurso {recurso.Nome} criado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Erro ao criar o recurso: {response.StatusCode}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao criar o recurso: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task AtualizarRecursoAsync(Recurso recurso)
        {
            try
            {
                var apiUrl = $"http://localhost:3000/Api/UpdateRecurso/{recurso.Id}";
                var response = await _httpClient.PutAsJsonAsync(apiUrl, recurso);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Recurso {recurso.Nome} atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                         }
                else
                {
                    MessageBox.Show($"Erro ao salvar o recurso: {response.StatusCode}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                // Captura exceções e mostra a mensagem de erro
                Console.WriteLine($"Erro ao atualizar o recurso: {ex.Message}");
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
