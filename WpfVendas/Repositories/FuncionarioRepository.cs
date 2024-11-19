using WpfVendas.Repositories;
using DsiVendas.Models;

namespace WpfVendas.Repositories
{
    public class FuncionarioRepository : Repository<Funcionario>
    {
        public Funcionario GetById(int id)
        {
            return items.FirstOrDefault(f => f.Id == id);
        }

        public IQueryable<Funcionario> GetFuncionariosPorNome(string nome)
        {
            return items.Where(f => f.Nome == nome).AsQueryable();
        }
    }
}
