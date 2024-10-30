namespace DsiVendas.Models;

public class Fornecedor
{
  public int Id { get; set; }
  public string CNPJ { get; set; } = string.Empty;
  public string RazaoSocial { get; set; } = string.Empty;
  public string Endereco { get; set; } = string.Empty;
  public string Telefone { get; set; } = string.Empty;
}
