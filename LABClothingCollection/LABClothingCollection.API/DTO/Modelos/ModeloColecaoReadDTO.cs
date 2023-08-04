using LABClothingCollection.API.Enums;

//DTO utilizado para transferir dados entre camadas da aplicação ou sistemas.
//DTOs são objetos simples que carregam apenas os dados necessários para uma operação específica,
//evitando transferir informações desnecessárias e melhorando o desempenho da aplicação.
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
