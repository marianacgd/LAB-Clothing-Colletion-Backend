using LABClothingCollection.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace LABClothingCollection.API.DTO.Colecoes
{
    public class ColecaoCreateDTO : IValidatableObject
    {
        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(100, ErrorMessage = "Este campo aceita até 100 caracteres")]
        [MinLength(3, ErrorMessage = "Favor digitar o nome da coleção")]
        [Display(Name = "Nome da Coleção")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(20, ErrorMessage = "Este campo aceita até 20 caracteres")]
        [MinLength(2, ErrorMessage = "Favor digitar a marca")]
        [Display(Name = "Marca")]
        public string Marca { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Orçamento")]
        public decimal Orcamento { get; set; }

        [Required]
        [Range(1900, 9999, ErrorMessage = "O valor deve ser um ano válido no formato YYYY.")]
        [Display(Name = "Ano de Lançamento")]
        public int AnoLancamento { get; set; }

        [Required]
        [Display(Name = "Estação")]
        public EstacaoEnum Estacao { get; set; }

        [Required]
        [Display(Name = "Status do Sistema")]
        public StatusEnum StatusSistema { get; set; }

        [Required]
        [Display(Name = "Responsável")]
        public int ResponsavelId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> lista = new List<ValidationResult>();

            if (!Enum.IsDefined(typeof(EstacaoEnum), Estacao))
            {
                lista.Add(new ValidationResult($"Erro no seleção da Estação", new[] { nameof(Estacao) }));
            }

            if (!Enum.IsDefined(typeof(StatusEnum), StatusSistema))
            {
                lista.Add(new ValidationResult($"Erro no status", new[] { nameof(StatusSistema) }));
            }

            return lista;
        }
    }
}
