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
    public class FazendaViewModel : INotifyPropertyChanged
    {
        private readonly HttpClient _httpClient;

        public ObservableCollection<Fazenda> Fazenda { get; set; }

        public FazendaViewModel()
        {
            _httpClient = new HttpClient();
            Fazenda = new ObservableCollection<Fazenda>();
   
        }

        public async Task CarregarFazendaDaAPI()
        {
            try
            {
                var apiUrl = "http://localhost:5299/Api/GetFazenda";
                var fazendaDaApi = await _httpClient.GetFromJsonAsync<Fazenda[]>(apiUrl);

                if (fazendaDaApi != null)
                {
                    foreach (var fazenda in fazendaDaApi)
                    {
                        Fazenda.Add(fazenda);
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Erro ao buscar fazenda: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
