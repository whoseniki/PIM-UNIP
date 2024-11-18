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
    internal class ItemVendaCadastroViewModel : INotifyPropertyChanged
    {
        private readonly HttpClient _httpClient;
        private ItemVenda _itemVenda;

        public ItemVenda ItemVenda
        {
            get => _itemVenda;
            set
            {
                _itemVenda = value;
                OnPropertyChanged();
            }
        }

        public ICommand SalvarCommand { get; }
        public ICommand CancelarCommand { get; }

        private readonly Action _fecharAction;

        public ItemVendaCadastroViewModel(Action fecharAction, ItemVenda itemVenda = null)
        {
            _fecharAction = fecharAction;
            ItemVenda = itemVenda ?? new ItemVenda(); // Se o cliente for null, criamos um novo.

            // Inicializa o HttpClient (aqui ou injetado)
            _httpClient = new HttpClient();

            SalvarCommand = new RelayCommand(SalvarItemVenda);
            CancelarCommand = new RelayCommand(Cancelar);
        }

        private async void SalvarItemVenda(object obj)
        {
            if (ItemVenda != null)
            {
                if (ItemVenda.Id == 0)
                {
                    await CriarItemVendaAsync(ItemVenda);
                }
                else
                {
                    await AtualizarItemVendaAsync(ItemVenda);
                }

                _fecharAction(); 
            }
        }

        private async Task CriarItemVendaAsync(ItemVenda itemVenda)
        {
            try
            {
                var apiUrl = "http://localhost:5299/Api/CreateItemVenda";
                var response = await _httpClient.PostAsJsonAsync(apiUrl, itemVenda);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Item Venda {itemVenda.Id} criado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Erro ao criar o Item Venda: {response.StatusCode}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao criar o Item Venda: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task AtualizarItemVendaAsync(ItemVenda itemVenda)
        {
            try
            {
                var apiUrl = $"http://localhost:5299/Api/UpdateItemVenda/{itemVenda.Id}";
                var response = await _httpClient.PutAsJsonAsync(apiUrl, itemVenda);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Item Venda {itemVenda.Id} atualizado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show($"Erro ao atualizar o Item Venda: {response.StatusCode}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar o Item Venda: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Cancelar(object obj)
        {
            _fecharAction();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
