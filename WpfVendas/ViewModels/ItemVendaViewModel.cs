using DsiVendas.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfVendas.ViewModels
{
    public class ItemVendaViewModel : INotifyPropertyChanged
    {
        private readonly HttpClient _httpClient;

        public ObservableCollection<ItemVenda> ItemVenda { get; set; }

        public ItemVendaViewModel()
        {
            _httpClient = new HttpClient();
            ItemVenda = new ObservableCollection<ItemVenda>();
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
