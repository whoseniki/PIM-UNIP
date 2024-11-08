namespace DsiVendas.Models;

public class Recurso
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public decimal Quantidade { get; set; }

    public int FornecedorId { get; set; }
    public Fornecedor Fornecedor { get; set; }
    public string TipoDeRecurso { get; set; }

}
