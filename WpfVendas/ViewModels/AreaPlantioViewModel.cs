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
    public class AreaPlantioViewModel : INotifyPropertyChanged
    {
        private readonly HttpClient _httpClient;

        public ObservableCollection<AreaPlantio> AreaPlantio { get; set; }

        public AreaPlantioViewModel()
        {
            _httpClient = new HttpClient();
            AreaPlantio = new ObservableCollection<AreaPlantio>();
            
        }

        public async Task CarregarAreaPlantioDaAPI()
        {
            try
            {
                var apiUrl = "http://localhost:5299/Api/GetAreaPlantio";
                var areaPlantioDaApi = await _httpClient.GetFromJsonAsync<AreaPlantio[]>(apiUrl);

                if (areaPlantioDaApi != null)
                {
                    foreach (var areaPlantio in areaPlantioDaApi)
                    {
                        AreaPlantio.Add(areaPlantio);
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Erro ao buscar Área de Plantio: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
