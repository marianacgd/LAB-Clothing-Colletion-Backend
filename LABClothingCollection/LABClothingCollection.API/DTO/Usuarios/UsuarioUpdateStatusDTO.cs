using LABClothingCollection.API.Enums;
using System.ComponentModel.DataAnnotations;

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
