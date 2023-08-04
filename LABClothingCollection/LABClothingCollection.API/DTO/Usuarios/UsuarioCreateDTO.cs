using LABClothingCollection.API.Enums;
using System.ComponentModel.DataAnnotations;

//DTO utilizado para transferir dados entre camadas da aplicação ou sistemas.
//DTOs são objetos simples que carregam apenas os dados necessários para uma operação específica,
//evitando transferir informações desnecessárias e melhorando o desempenho da aplicação.
namespace LABClothingCollection.API.DTO.Usuarios
{
    public class UsuarioCreateDTO : IValidatableObject
    {
        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(250, ErrorMessage = "Este campo aceita até 250 caracteres")]
        [MinLength(3, ErrorMessage = "Favor digitar o nome completo")]
        [Display(Name = "Nome Completo")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Gênero")]
        public GeneroEnum Genero { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Data de Nascimento")]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(18, ErrorMessage = "Este campo aceita até 18 caracteres")]
        [MinLength(11, ErrorMessage = "Favor digitar o minimo de 11 caracteres")]
        [Display(Name = "CPF/CNPJ")]
        public string Documento { get; set; }

        [MaxLength(11, ErrorMessage = "Este campo aceita até 11 caracteres")]
        [MinLength(11, ErrorMessage = "Favor digitar um número de telefone")]
        [Display(Name = "Telefone")]
        public string? Telefone { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "E-mail")]
        [EmailAddress(ErrorMessage = "Email Erradp")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Tipo de Usuário")]
        public TipoUsuarioEnum Tipo { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Status de Usuário")]
        public StatusEnum Status { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> lista  = new List<ValidationResult>();

            if (!Enum.IsDefined(typeof(GeneroEnum), Genero))
            {
                lista.Add(new ValidationResult($"Erro no Genero", new[] { nameof(Genero) }));
            }

            if (!Enum.IsDefined(typeof(TipoUsuarioEnum), Tipo))
            {
                lista.Add(new ValidationResult($"Erro no Tipo", new[] { nameof(Tipo) }));
            }

            if (!Enum.IsDefined(typeof(StatusEnum), Status))
            {
                lista.Add(new ValidationResult($"Erro no status", new[] { nameof(Status) }));
            }

            return lista;
        }
    }
}
