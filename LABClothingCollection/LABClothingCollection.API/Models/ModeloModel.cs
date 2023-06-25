using LABClothingCollection.API.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LABClothingCollection.API.Models
{

    [Table("Modelo")]
    [Index("Nome", IsUnique = true)]
    public class ModeloModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "VARCHAR"), Required, StringLength(100)]
        public string Nome { get; set; }

        [Required]
        public TipoEnum Tipo { get; set; }

        [Required]
        public LayoutEnum Layout { get; set; }

        public int ColecaoId { get; set; }

        [Required]
        public ColecaoModel Colecao { get; set; }
    }
}
