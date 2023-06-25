using LABClothingCollection.API.DTO.Colecoes;
using LABClothingCollection.API.DTO.Usuarios;
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
        public ModeloColecaoReadDTO Colecao { get; set; }
    }
}
