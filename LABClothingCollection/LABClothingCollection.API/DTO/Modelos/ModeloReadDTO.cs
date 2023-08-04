using LABClothingCollection.API.DTO.Colecoes;
using LABClothingCollection.API.DTO.Usuarios;
using LABClothingCollection.API.Enums;
using LABClothingCollection.API.Models;

//DTO utilizado para transferir dados entre camadas da aplicação ou sistemas.
//DTOs são objetos simples que carregam apenas os dados necessários para uma operação específica,
//evitando transferir informações desnecessárias e melhorando o desempenho da aplicação.
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
