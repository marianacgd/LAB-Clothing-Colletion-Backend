using LABClothingCollection.API.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LABClothingCollection.API.Models
{
    [Table("Colecao")] //anotação p especificar o nome da tabela na base de dados que será associada a esta entidade.
    [Index("Nome", IsUnique = true)] //anotação para definir um índice na coluna Nome da tabela. O parâmetro IsUnique = true indica que o índice será único, não pode haver dois registros com o mesmo valor na coluna Nome.
    public class ColecaoModel
    {
        [Key] //anotação para indicar q esta propriedade é a chave primária da tabela na base de dados.
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //autoincremento - anotação utilizada para especificar que o valor da chave primária será gerado automaticamente pela base de dados. 
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

        public int ResponsavelId { get; set; } //propriedade q representa o id do responsável que pertence a coleção.

        [Required]
        public virtual UsuarioModel Responsavel { get; set; }  //propriedade de navegação que representa um responsável associado a coleção.

        public virtual ICollection<ModeloModel>? Modelos { get; set; }
        //propriedade de navegação que representa uma coleção de objetos ModeloModel.
        //virtual = permite q realize o carregamento lento, o que significa que os dados da coleção serão carregados apenas quando forem necessários.
    }
}
//ColecaoModel entidade que será mapeada para uma tabela chamada "Colecao" na base de dados.
//tem propriedades id, nome, marca, orcamento, anoLancamento, estacaoEnum, statusEnum, assim como a relação com o responsavel a que pertence a colecao e relação de modelos que a coleção contem. 
//As propriedades de enum utilizam enumerações para representar valores predefinidos que podem ser usados.