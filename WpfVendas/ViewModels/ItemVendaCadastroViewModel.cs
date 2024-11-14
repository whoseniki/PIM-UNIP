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
             _fecharAction();
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
