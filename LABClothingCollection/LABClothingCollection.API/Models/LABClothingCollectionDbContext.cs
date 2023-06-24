using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;

namespace LABClothingCollection.API.Models
{
    /// <summary>
    /// Iteração com o banco de dados, nas tabelas utilizando o DBSet
    /// </summary>
    public class LABClothingCollectionDbContext : DbContext
    {
        public LABClothingCollectionDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<UsuarioModel> Usuarios { get; set; }

    }
}
