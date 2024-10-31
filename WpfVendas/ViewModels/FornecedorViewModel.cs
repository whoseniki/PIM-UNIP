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
    public class FornecedorViewModel : INotifyPropertyChanged
    {
        private readonly HttpClient _httpClient;

        public ObservableCollection<Fornecedor> Fornecedores { get; set; }

        public FornecedorViewModel()
        {
            _httpClient = new HttpClient();
            Fornecedores = new ObservableCollection<Fornecedor>();
   
        }

        public async Task CarregarFornecedoresDaAPI()
        {
            try
            {
                var apiUrl = "http://localhost:5299/Api/GetFornecedores";
                var fornecedoresDaApi = await _httpClient.GetFromJsonAsync<Fornecedor[]>(apiUrl);

                if (fornecedoresDaApi != null)
                {
                    foreach (var fornecedor in fornecedoresDaApi)
                    {
                        Fornecedores.Add(fornecedor);
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Erro ao buscar fornecedores: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
