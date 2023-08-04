using LABClothingCollection.API.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LABClothingCollection.API.Models
{

    [Table("Modelo")] //anotação p especificar o nome da tabela na base de dados que será associada a esta entidade.
    [Index("Nome", IsUnique = true)]  //anotação para definir um índice na coluna Nome da tabela. O parâmetro IsUnique = true indica que o índice será único, não pode haver dois registros com o mesmo valor na coluna Nome.
    public class ModeloModel
    {
        [Key] //anotação para indicar q esta propriedade é a chave primária da tabela na base de dados.
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //autoincremento - anotação utilizada para especificar que o valor da chave primária será gerado automaticamente pela base de dados. 
        public int Id { get; set; }

        [Column(TypeName = "VARCHAR"), Required, StringLength(100)]
        public string Nome { get; set; }

        [Required]
        public TipoEnum Tipo { get; set; }

        [Required]
        public LayoutEnum Layout { get; set; }

        public int ColecaoId { get; set; } //propriedade q representa o id da coleção que pertence o modelo.

        [Required]
        public ColecaoModel Colecao { get; set; } //propriedade de navegação que representa uma coleção associada ao modelo.
    }
}
//ModeloModel entidade que será mapeada para uma tabela chamada "Modelo" na base de dados.
//tem propriedades id, nome, tipoEnum, layoutEnum, assim como a relação com a coleção a que pertence o modelo. 
//As propriedades de enum utilizam enumerações para representar valores predefinidos que podem ser usados.