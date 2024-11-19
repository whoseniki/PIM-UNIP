using WpfVendas.Repositories;
using DsiVendas.Models;

namespace WpfVendas.Repositories
{
    public class ItemCompraRepository : Repository<ItemCompra>
    {
        public ItemCompra GetById(int id)
        {
            return items.FirstOrDefault(i => i.Id == id);
        }

        public IQueryable<ItemCompra> GetItensCompraPorId(int id)
        {
            return items.Where(i => i.Id == id).AsQueryable();
        }
    }
}
