using LABClothingCollection.API.Enums;
using System.ComponentModel.DataAnnotations;

//DTO utilizado para transferir dados entre camadas da aplicação ou sistemas.
//DTOs são objetos simples que carregam apenas os dados necessários para uma operação específica,
//evitando transferir informações desnecessárias e melhorando o desempenho da aplicação.
namespace LABClothingCollection.API.DTO.Colecoes
{
    public class ColecaoUpdateStatusDTO : IValidatableObject
    {
        [Display(Name = "Status da Coleção")]
        [Required(ErrorMessage = "Campo Obrigatório")]
        public StatusEnum StatusSistema { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Enum.IsDefined(typeof(StatusEnum), StatusSistema))
            {
                yield return new ValidationResult($"Erro no status da coleção", new[] { nameof(StatusSistema) });
            }
        }
    }
}
