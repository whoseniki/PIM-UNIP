namespace DsiVendas.Models;

public class Manutencao
{
    public int Id { get; set; }

    public int RecursoId { get; set; }
    public Recurso Recurso { get; set; }

    public int FuncionarioId { get; set; }
    public Funcionario Funcionario { get; set; }

}
