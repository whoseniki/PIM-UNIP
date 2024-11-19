using WpfVendas.Repositories;
using DsiVendas.Models;

namespace WpfVendas.Repositories
{
    public class PlantioRepository : Repository<Plantio>
    {
        public Plantio GetById(int id)
        {
            return items.FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<Plantio> GetPlantiosPorId(int id)
        {
            return items.Where(p => p.Id == id).AsQueryable();
        }
    }
}
