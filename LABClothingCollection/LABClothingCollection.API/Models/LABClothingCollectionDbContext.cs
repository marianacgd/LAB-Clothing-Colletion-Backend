using Microsoft.EntityFrameworkCore;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ColecaoModel>().HasOne(e => e.Responsavel)
                        .WithMany(x => x.Colecao)
                        .Metadata
                        .DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.Entity<ModeloModel>().HasOne(e => e.Colecao)
                        .WithMany(x => x.Modelos)
                        .Metadata
                        .DeleteBehavior = DeleteBehavior.Restrict;
        }

        public virtual DbSet<UsuarioModel> Usuarios { get; set; }
        public virtual DbSet<ColecaoModel> Colecoes { get; set; }
        public virtual DbSet<ModeloModel> Modelos { get; set; }

    }
}
