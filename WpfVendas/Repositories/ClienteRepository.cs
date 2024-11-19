using WpfVendas.Repositories;
using DsiVendas.Models;

namespace WpfVendas.Repositories
{
    public class ClienteRepository : Repository<Cliente>
    {
        public Cliente GetById(int id)
        {
            return items.FirstOrDefault(c => c.Id == id);
        }

        public IQueryable<Cliente> GetClientesPorNome(string nome)
        {
            return items.Where(c => c.Nome == nome).AsQueryable();
        }
    }
}