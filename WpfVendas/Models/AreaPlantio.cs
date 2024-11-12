namespace DsiVendas.Models
{
    public class AreaPlantio
    {
        public int Id { get; set; }

        public int PlantioId { get; set; }
        public Plantio Plantio { get; set; }

        public string Nome { get; set; }
        public string Localizacao { get; set; }
        public int Hectares { get; set; }
        public DateTime DataPlantio { get; set; }
        
    }
}