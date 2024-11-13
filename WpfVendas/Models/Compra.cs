namespace DsiVendas.Models
{
    public class Compra
    {
        public int Id { get; set; }

        public int FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }

        public int FuncionarioId { get; set; }
        public Funcionario Funcionario { get; set; }

        public DateTime DataVenda { get; set; }
        public Venda Venda { get; set; }

        public string Status { get; set; }
        public string FormaPagamento { get; set; }
        public string FormaEntrega { get; set; }
        public decimal Total => ItemCompra?.Sum(item => item.SubTotal) ?? 0;
        public ICollection<ItemCompra> ItemCompra { get; set; }
    }
}