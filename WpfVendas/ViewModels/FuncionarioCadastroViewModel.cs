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
    internal class FuncionarioCadastroViewModel : INotifyPropertyChanged
    {
        private readonly HttpClient _httpClient;
        private Funcionario _funcionario;

        public Funcionario Funcionario
        {
            get => _funcionario;
            set
            {
                _funcionario = value;
                OnPropertyChanged();
            }
        }

        public ICommand SalvarCommand { get; }
        public ICommand CancelarCommand { get; }

        private readonly Action _fecharAction;

        public FuncionarioCadastroViewModel(Action fecharAction, Funcionario funcionario = null)
        {
            _fecharAction = fecharAction;
            Funcionario = funcionario ?? new Funcionario(); // Se o cliente for null, criamos um novo.

            // Inicializa o HttpClient (aqui ou injetado)
            _httpClient = new HttpClient();

            SalvarCommand = new RelayCommand(SalvarFuncionario);
            CancelarCommand = new RelayCommand(Cancelar);
        }

        private async void SalvarFuncionario(object obj)
        {
            if (Funcionario != null)
            {
                if (Funcionario.Id == 0)
                {
                    await CriarFuncionarioAsync(Funcionario);
                }
                else
                {
                    await AtualizarFuncionarioAsync(Funcionario);
                }

                _fecharAction(); // Fecha a janela após salvar
            }
        }

        private async Task CriarFuncionarioAsync(Funcionario funcionario)
        {
            try
            {
                var apiUrl = "http://localhost:5299/Api/CreateFuncionario";
                var response = await _httpClient.PostAsJsonAsync(apiUrl, funcionario);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Funcionario {funcionario.Nome} criado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Erro ao criar o funcionario: {response.StatusCode}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao criar o funcionario: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task AtualizarFuncionarioAsync(Funcionario funcionario)
        {
            try
            {
                var apiUrl = $"http://localhost:5299/Api/UpdateFuncionario/{funcionario.Id}";
                var response = await _httpClient.PutAsJsonAsync(apiUrl, funcionario);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Funcionário {funcionario.Nome} atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Erro ao atualizar o funcionario: {response.StatusCode}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar o funcionario: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
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
