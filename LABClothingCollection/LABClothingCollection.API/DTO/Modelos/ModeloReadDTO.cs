using LABClothingCollection.API.Enums;
using LABClothingCollection.API.Models;

namespace LABClothingCollection.API.DTO.Modelos
{
    public class ModeloReadDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public TipoEnum Tipo { get; set; }
        public LayoutEnum Layout { get; set; }
        public ColecaoModel Colecao { get; set; }
    }
}
