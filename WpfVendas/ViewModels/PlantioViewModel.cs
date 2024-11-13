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
    public class PlantioViewModel : INotifyPropertyChanged
    {
        private readonly HttpClient _httpClient;

        public ObservableCollection<Plantio> Plantio { get; set; }

        public PlantioViewModel()
        {
            _httpClient = new HttpClient();
            Plantio = new ObservableCollection<Plantio>();
            
        }

        public async Task CarregarPlantioDaAPI()
        {
            try
            {
                var apiUrl = "http://localhost:5299/Api/GetPlantio";
                var plantioDaApi = await _httpClient.GetFromJsonAsync<Plantio[]>(apiUrl);

                if (plantioDaApi != null)
                {
                    foreach (var plantio in plantioDaApi)
                    {
                        Plantio.Add(plantio);
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Erro ao buscar Plantio: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
