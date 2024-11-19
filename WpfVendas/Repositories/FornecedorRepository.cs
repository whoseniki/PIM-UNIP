using System.Linq;

using DsiVendas.Models;

namespace WpfVendas.Repositories
{
    public class FornecedorRepository : Repository<Fornecedor>
    {
        public Fornecedor GetById(int id)
        {
            return items.FirstOrDefault(f => f.Id == id);  // Supondo que 'items' seja a coleção de fornecedores
        }

        // Exemplo de método adicional que poderia ser útil
        public IQueryable<Fornecedor> GetFornecedoresPorRazaoSocial(string razaoSocial)
        {
            return items.Where(f => f.RazaoSocial == razaoSocial).AsQueryable();
        }
    }
}
