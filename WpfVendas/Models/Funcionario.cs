namespace DsiVendas.Models
{
    public class Funcionario
    {
        public int Id { get; set; }

        public int FazendaId { get; set; }
        public Fazenda Fazenda { get; set; }

        public string? Nome { get; set; }
        public string? CPF { get; set; }
        public string? Telefone { get; set; }
        public string? Email { get; set; }
        public string? TipoDeFuncao { get; set; }
        public decimal Salario { get; set; }
        public int Senha { get; set; }
        
    }
}