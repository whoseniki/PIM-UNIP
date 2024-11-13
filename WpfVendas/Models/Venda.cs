namespace DsiVendas.Models
{
    public class Venda
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public DateTime DataVenda { get; set; }
        public string FormaPagamento { get; set; }
        
    }
}