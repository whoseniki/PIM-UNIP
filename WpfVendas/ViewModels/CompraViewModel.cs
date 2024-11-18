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
    public class CompraViewModel : INotifyPropertyChanged
    {
        private readonly HttpClient _httpClient;

        public ObservableCollection<Compra> Compra { get; set; }

        public CompraViewModel()
        {
            _httpClient = new HttpClient();
            Compra = new ObservableCollection<Compra>();
            
        }

        public async Task CarregarCompraDaAPI()
        {
            try
            {
                var apiUrl = "http://localhost:5299/Api/GetCompra";
                var compraDaApi = await _httpClient.GetFromJsonAsync<Compra[]>(apiUrl);

                if (compraDaApi != null)
                {
                    foreach (var compra in compraDaApi)
                    {
                        Compra.Add(compra);
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Erro ao buscar compra: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
