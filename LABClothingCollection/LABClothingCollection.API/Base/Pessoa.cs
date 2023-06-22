using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LABClothingCollection.API.Base
{
    public class Pessoa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "VARCHAR"), Required, StringLength(250)]
        public string NomeCompleto { get; set; }

        [Column(TypeName = "CHAR"), Required, StringLength(1)]
        public string Genero { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }

        [Column(TypeName = "VARCHAR"), Required, StringLength(18)]
        public string Documento { get; set; }

        [Column(TypeName = "VARCHAR"), Required, StringLength(11)]
        public string? Telefone { get; set; }
    }
}
