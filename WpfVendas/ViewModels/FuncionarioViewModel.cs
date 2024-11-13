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
    public class FuncionarioViewModel : INotifyPropertyChanged
    {
        private readonly HttpClient _httpClient;

        public ObservableCollection<Funcionario> Funcionario { get; set; }

        public FuncionarioViewModel()
        {
            _httpClient = new HttpClient();
            Funcionario = new ObservableCollection<Funcionario>();
            //CarregarClientesDaAPI();
        }

        public async Task CarregarFuncionarioDaAPI()
        {
            try
            {
                var apiUrl = "http://localhost:5299/Api/GetFuncionario";
                var funcionarioDaApi = await _httpClient.GetFromJsonAsync<Funcionario[]>(apiUrl);

                if (funcionarioDaApi != null)
                {
                    foreach (var funcionario in funcionarioDaApi)
                    {
                        Funcionario.Add(funcionario);
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Erro ao buscar funcionario: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
