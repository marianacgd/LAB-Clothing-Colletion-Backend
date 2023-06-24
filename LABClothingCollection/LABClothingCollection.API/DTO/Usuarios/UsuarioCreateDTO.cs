using LABClothingCollection.API.Enums;
using System.ComponentModel.DataAnnotations;

namespace LABClothingCollection.API.DTO.Usuarios
{
    public class UsuarioCreateDTO
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

        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(11, ErrorMessage = "Este campo aceita até 11 caracteres")]
        [MinLength(11, ErrorMessage = "Favor digitar um número de telefone")]
        [Display(Name = "Telefone")]
        public string? Telefone { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(50, ErrorMessage = "Este campo aceita até 50 caracteres")]
        [MinLength(3, ErrorMessage = "Favor digitar o e-mail")]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Tipo de Usuário")]
        public TipoUsuarioEnum Tipo { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [Display(Name = "Status de Usuário")]
        public StatusEnum Status { get; set; }
    }
}
