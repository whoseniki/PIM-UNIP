using WpfVendas.Repositories;
using DsiVendas.Models;

namespace WpfVendas.Repositories
{
    public class VendaRepository : Repository<Venda>
    {
        public Venda GetById(int id)
        {
            return items.FirstOrDefault(v => v.Id == id);
        }

        public IQueryable<Venda> GetVendasPorId(int id)
        {
            return items.Where(v => v.Id == id).AsQueryable();
        }
    }
}
