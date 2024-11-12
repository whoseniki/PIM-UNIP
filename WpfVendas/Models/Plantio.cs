namespace DsiVendas.Models
{
    public class Plantio
    {

        public int Id { get; set; }

        public int AreaPlantioId { get; set; }
        public AreaPlantio AreaPlantio { get; set; }

        public string? Nome { get; set; }
        public DateTime DataPlantio { get; set; }
    }
}