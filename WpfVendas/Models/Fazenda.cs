namespace DsiVendas.Models
{
    public class Fazenda
    {
        public int Id { get; set; }

        public int FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; }

        public string Nome { get; set; }
        public int Hectares { get; set; }
        public string Endereço { get; set; }
        public int Telefone { get; set; }
        public string Email { get; set; }
        
    }
}