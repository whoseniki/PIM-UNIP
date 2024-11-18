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
    public class ManutencaoViewModel : INotifyPropertyChanged
    {
        private readonly HttpClient _httpClient;

        public ObservableCollection<Manutencao> Manutencao { get; set; }

        public ManutencaoViewModel()
        {
            _httpClient = new HttpClient();
            Manutencao = new ObservableCollection<Manutencao>();
            
        }

        public async Task CarregarManutencaoDaAPI()
        {
            try
            {
                var apiUrl = "http://localhost:5299/Api/GetManutencao";
                var manutencaoDaApi = await _httpClient.GetFromJsonAsync<Manutencao[]>(apiUrl);

                if (manutencaoDaApi != null)
                {
                    foreach (var manutencao in manutencaoDaApi)
                    {
                        Manutencao.Add(manutencao);
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Erro ao buscar manutencao: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
