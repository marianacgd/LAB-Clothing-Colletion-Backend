using LABClothingCollection.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace LABClothingCollection.API.DTO.Modelos
{
    public class ModeloDTO : IValidatableObject
    {
        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(100, ErrorMessage = "Este campo aceita até 100 caracteres")]
        [MinLength(3, ErrorMessage = "Favor digitar o nome do modelo")]
        [Display(Name = "Nome do modelo")]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Tipo do modelo")]
        public TipoEnum Tipo { get; set; }

        [Required]
        [Display(Name = "Layout do modelo")]
        public LayoutEnum Layout { get; set; }

        [Required]
        [Display(Name = "Coleção")]
        public int ColecaoId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> lista = new List<ValidationResult>();

            if (!Enum.IsDefined(typeof(TipoEnum), Tipo))
            {
                lista.Add(new ValidationResult($"Erro no seleção do tipo", new[] { nameof(Tipo) }));
            }

            if (!Enum.IsDefined(typeof(LayoutEnum), Layout))
            {
                lista.Add(new ValidationResult($"Erro no layout", new[] { nameof(Layout) }));
            }

            return lista;
        }
    }
}
