using LABClothingCollection.API.Enums;
using System.ComponentModel.DataAnnotations;

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
