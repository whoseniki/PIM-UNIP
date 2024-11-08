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
    public class RecursoViewModel : INotifyPropertyChanged
    {
        private readonly HttpClient _httpClient;

        public ObservableCollection<Recurso> Recurso { get; set; }

        public RecursoViewModel()
        {
            _httpClient = new HttpClient();
            Recurso = new ObservableCollection<Recurso>();
   
        }

        public async Task CarregarRecursoDaAPI()
        {
            try
            {
                var apiUrl = "http://localhost:5299/Api/GetRecurso";
                var recursoDaApi = await _httpClient.GetFromJsonAsync<Recurso[]>(apiUrl);

                if (recursoDaApi != null)
                {
                    foreach (var recurso in recursoDaApi)
                    {
                        Recurso.Add(recurso);
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Erro ao buscar recursos: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
