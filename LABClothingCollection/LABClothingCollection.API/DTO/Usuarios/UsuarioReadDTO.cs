using LABClothingCollection.API.Enums;
using System.ComponentModel.DataAnnotations;

//DTO utilizado para transferir dados entre camadas da aplicação ou sistemas.
//DTOs são objetos simples que carregam apenas os dados necessários para uma operação específica,
//evitando transferir informações desnecessárias e melhorando o desempenho da aplicação.
namespace LABClothingCollection.API.DTO.Usuarios
{
    public class UsuarioReadDTO
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; }
        public GeneroEnum Genero { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Documento { get; set; }
        public string? Telefone { get; set; }
        public string Email { get; set; }
        public TipoUsuarioEnum Tipo { get; set; }
        public StatusEnum Status { get; set; }
    }
}
