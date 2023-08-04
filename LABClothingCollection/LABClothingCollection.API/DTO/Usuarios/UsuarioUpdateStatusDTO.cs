using LABClothingCollection.API.Enums;
using System.ComponentModel.DataAnnotations;

//DTO utilizado para transferir dados entre camadas da aplicação ou sistemas.
//DTOs são objetos simples que carregam apenas os dados necessários para uma operação específica,
//evitando transferir informações desnecessárias e melhorando o desempenho da aplicação.
namespace LABClothingCollection.API.DTO.Usuarios
{
    public class UsuarioUpdateStatusDTO : IValidatableObject
    {
        [Display(Name = "Status de Usuário")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public StatusEnum Status { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Enum.IsDefined(typeof(StatusEnum), Status))
            {
                yield return new ValidationResult($"Erro no status", new[] { nameof(Status) });
            }
        }
    }
}
