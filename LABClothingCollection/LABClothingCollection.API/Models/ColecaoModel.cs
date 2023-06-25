using LABClothingCollection.API.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LABClothingCollection.API.Models
{
    [Table("Colecao")]
    [Index("Nome", IsUnique = true)]
    public class ColecaoModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Column(TypeName = "VARCHAR"), Required, StringLength(100)]
        public string Nome { get; set; }
        
        [Column(TypeName = "VARCHAR"), Required, StringLength(20)]
        public string Marca { get; set; }
        
        [Required]
        public decimal Orcamento { get; set; }
        
        [Required]
        public int AnoLancamento { get; set; }
 
        [Required]
        public EstacaoEnum Estacao { get; set; }
 
        [Required]
        public StatusEnum StatusSistema { get; set; }

        public int ResponsavelId { get; set; }

        [Required]
        public virtual UsuarioModel Responsavel { get; set; }

        public virtual ICollection<ModeloModel>? Modelos { get; set; }
    }
}
