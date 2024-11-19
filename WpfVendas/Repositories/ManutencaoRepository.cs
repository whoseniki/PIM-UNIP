using WpfVendas.Repositories;
using DsiVendas.Models;

namespace WpfVendas.Repositories
{
    public class ManutencaoRepository : Repository<Manutencao>
    {
        public Manutencao GetById(int id)
        {
            return items.FirstOrDefault(m => m.Id == id);
        }

        public IQueryable<Manutencao> GetManutencoesPorId(int id)
        {
            return items.Where(m => m.Id == id).AsQueryable();
        }
    }
}
