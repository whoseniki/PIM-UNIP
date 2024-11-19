using WpfVendas.Repositories;
using DsiVendas.Models;

namespace WpfVendas.Repositories
{
    public class AreaPlantioRepository : Repository<AreaPlantio>
    {
        public AreaPlantio GetById(int id)
        {
            return items.FirstOrDefault(a => a.Id == id);
        }

        public IQueryable<AreaPlantio> GetAreasPlantioPorId(int id)
        {
            return items.Where(a => a.Id == id).AsQueryable();
        }
    }
}
