namespace DsiVendas.Models
{
    public class ItemVenda
    {
        public int Id { get; set; }

        public int VendaId { get; set; }
        public Venda Venda { get; set; }

        public int RecursoId { get; set; }
        public Recurso Recurso { get; set; }

        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal SubTotal => Quantidade * PrecoUnitario;
    }
}