using LABClothingCollection.API.Base;
using LABClothingCollection.API.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LABClothingCollection.API.Models
{
    [Table("Usuario")] //anotação p especificar o nome da tabela na base de dados que será associada a esta entidade. 
    [Index("Email", IsUnique = true)] //anotação para definir um índice na coluna Email da tabela. O parâmetro IsUnique = true indica que o índice será único, não pode haver dois registros com o mesmo valor na coluna Email.
    public class UsuarioModel : Pessoa //Herança. UsuarioModel é uma subclasse de Pessoa.
    {
        [Column(TypeName = "VARCHAR"), Required, StringLength(50)]
        public string Email { get; set; }

        [Required]
        public TipoUsuarioEnum Tipo { get; set; }

        [Required]
        public StatusEnum Status { get; set; }

        public virtual ICollection<ColecaoModel>? Colecao { get; set; }
        //propriedade de navegação que representa uma coleção de objetos ColecaoModel.
        //virtual = permite q realize o carregamento lento, o que significa que os dados da coleção serão carregados apenas quando forem necessários.

    }
}

//UsuarioModel entidade que será mapeada para uma tabela chamada "Usuário" na base de dados.
//tem propriedades email, tipoUsuarioEnum, StatusEnum e uma coleção de coleções (relação um a muitos).
//As propriedades de enum utilizam enumerações para representar valores predefinidos que podem ser usados.