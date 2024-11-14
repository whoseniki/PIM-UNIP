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
    public class VendaViewModel : INotifyPropertyChanged
    {
        private readonly HttpClient _httpClient;

        public ObservableCollection<Venda> Venda { get; set; }

        public VendaViewModel()
        {
            _httpClient = new HttpClient();
            Venda = new ObservableCollection<Venda>();
            
        }

        public async Task CarregarVendaDaAPI()
        {
            try
            {
                var apiUrl = "http://localhost:5299/Api/GetVenda";
                var vendaDaApi = await _httpClient.GetFromJsonAsync<Venda[]>(apiUrl);

                if (vendaDaApi != null)
                {
                    foreach (var venda in vendaDaApi)
                    {
                        Venda.Add(venda);
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Erro ao buscar venda: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
