using WpfVendas.Repositories;
using DsiVendas.Models;

namespace WpfVendas.Repositories
{
    public class FazendaRepository : Repository<Fazenda>
    {
        public Fazenda GetById(int id)
        {
            return items.FirstOrDefault(f => f.Id == id);
        }

        public IQueryable<Fazenda> GetFazendasPorNome(string nome)
        {
            return items.Where(f => f.Nome == nome).AsQueryable();
        }
    }
}
