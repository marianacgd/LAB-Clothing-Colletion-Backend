using LABClothingCollection.API.Enums;

namespace LABClothingCollection.API.DTO.Modelos
{
    public class ModeloColecaoReadDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Marca { get; set; }
        public decimal Orcamento { get; set; }
        public int AnoLancamento { get; set; }
        public EstacaoEnum Estacao { get; set; }
        public StatusEnum StatusSistema { get; set; }
    }
}
