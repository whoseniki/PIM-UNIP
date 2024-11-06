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
    internal class ProdutoCadastroViewModel : INotifyPropertyChanged
    {

        private readonly HttpClient _httpClient;
        private Produto _produto;
        public Produto Produto
        {
            get => _produto;
            set
            {
                _produto = value;
                OnPropertyChanged();
            }
        }

        public ICommand SalvarCommand { get; }
        public ICommand CancelarCommand { get; }

        private readonly Action _fecharAction;

        public ProdutoCadastroViewModel(Action fecharAction, Produto produto = null)
        {
            _fecharAction = fecharAction;
            Produto = produto ?? new Produto(); 
            SalvarCommand = new RelayCommand(SalvarProduto);
            CancelarCommand = new RelayCommand(Cancelar);
        }

        private void SalvarProduto(object obj)
        {
            if (Produto != null)
            {
                if (Produto.Id == 0)
                {
                    CriarProduto(Produto);
                }
                else
                {
                    AtualizarProdutoAsync(Produto);
                }

                //_fecharAction();
            }
        }

        private void CriarProduto(Produto produto)
        {
            // Lógica para criação do cliente (incluir cliente na base de dados, etc.)
        }

        private async Task AtualizarProdutoAsync(Produto produto)
        {
            try
            {
                var apiUrl = $"http://localhost:3000/Api/UpdateProduto/{produto.Id}";
                var response = await _httpClient.PutAsJsonAsync(apiUrl, produto);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Produto {produto.Nome} atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                         }
                else
                {
                    MessageBox.Show($"Erro ao salvar o produto: {response.StatusCode}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                // Captura exceções e mostra a mensagem de erro
                Console.WriteLine($"Erro ao atualizar o produto: {ex.Message}");
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
