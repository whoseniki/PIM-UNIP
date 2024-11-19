using WpfVendas.Repositories;
using DsiVendas.Models;

namespace WpfVendas.Repositories
{
    public class ItemVendaRepository : Repository<ItemVenda>
    {
        public ItemVenda GetById(int id)
        {
            return items.FirstOrDefault(i => i.Id == id);
        }

        public IQueryable<ItemVenda> GetItensVendaPorId(int id)
        {
            return items.Where(i => i.Id == id).AsQueryable();
        }
    }
}
