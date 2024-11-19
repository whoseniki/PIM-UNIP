using System.Linq;
using DsiVendas.Models;

namespace WpfVendas.Repositories
{
    public class RecursoRepository : Repository<Recurso>
    {
        public Recurso GetById(int id)
        {
            return items.FirstOrDefault(r => r.Id == id);  // Supondo que 'items' seja a coleção de recursos
        }

        // Exemplo de método adicional que poderia ser útil
        public IQueryable<Recurso> GetRecursosPorTipo(string tipo)
        {
            return items.Where(r => r.TipoDeRecurso == tipo).AsQueryable();
        }
    }
}
