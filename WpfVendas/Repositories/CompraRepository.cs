using WpfVendas.Repositories;
using DsiVendas.Models;

namespace WpfVendas.Repositories
{
    public class CompraRepository : Repository<Compra>
    {
        public Compra GetById(int id)
        {
            return items.FirstOrDefault(c => c.Id == id);
        }

        public IQueryable<Compra> GetComprasPorId(int id)
        {
            return items.Where(c => c.Id == id).AsQueryable();
        }
    }
}
