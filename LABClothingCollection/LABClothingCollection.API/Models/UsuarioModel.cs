using LABClothingCollection.API.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace LABClothingCollection.API.Models
{
    [Table("Usuario")]
    [Index("Email", IsUnique = true)]
    public class UsuarioModel : Pessoa
    {
        [Column(TypeName = "VARCHAR"), Required, StringLength(50)]
        public string Email { get; set; }
    }
}
