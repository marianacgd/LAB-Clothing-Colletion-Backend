using LABClothingCollection.API.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;

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


            SeedDataUsuariosModel(modelBuilder);
            SeedDataColecoesModel(modelBuilder);
            SeedDataModelosModel(modelBuilder);
        }

        public virtual DbSet<UsuarioModel> Usuarios { get; set; }
        public virtual DbSet<ColecaoModel> Colecoes { get; set; }
        public virtual DbSet<ModeloModel> Modelos { get; set; }


        private static void SeedDataUsuariosModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioModel>().HasData(
              new UsuarioModel
              {
                  Id = 1,
                  NomeCompleto = "Mariana De Carvalho Gonçaves Daruix",
                  Documento = "23188028075",
                  DataNascimento = Convert.ToDateTime("1990-08-22"),
                  Email = "iancauerezende@htmail.com",
                  Genero = GeneroEnum.Feminino.GetDisplayName(),
                  Status = StatusEnum.Ativo,
                  Telefone = "86996448176",
                  Tipo = TipoUsuarioEnum.Administrador
              },
              new UsuarioModel
              {
                  Id = 2,
                  NomeCompleto = "Levi Murilo Caio Porto",
                  Documento = "25527501764",
                  DataNascimento = Convert.ToDateTime("1964-06-08"),
                  Email = "levi.murilo.porto@bidoul.eng.br",
                  Genero = GeneroEnum.Masculino.GetDisplayName(),
                  Status = StatusEnum.Inativo,
                  Telefone = "83995624145",
                  Tipo = TipoUsuarioEnum.Cridor
              },
              new UsuarioModel
              {
                  Id = 3,
                  NomeCompleto = "Isabella Marina Nunes",
                  Documento = "83934472000149",
                  DataNascimento = Convert.ToDateTime("2002-01-07"),
                  Email = "isabella_marina_nunes@uol.com.bt",
                  Genero = GeneroEnum.Feminino.GetDisplayName(),
                  Status = StatusEnum.Ativo,
                  Telefone = "48991551385",
                  Tipo = TipoUsuarioEnum.Gerente
              },
              new UsuarioModel
              {
                  Id = 4,
                  NomeCompleto = "Aline Brenda Freitas",
                  Documento = "81994451300",
                  DataNascimento = Convert.ToDateTime("1984-02-10"),
                  Email = "alinebrendafreitas@yaooll.com",
                  Genero = GeneroEnum.Feminino.GetDisplayName(),
                  Status = StatusEnum.Ativo,
                  Telefone = "11981604710",
                  Tipo = TipoUsuarioEnum.Outro
              },
                new UsuarioModel
                {
                    Id = 5,
                    NomeCompleto = "Vera Isadora Corte Real",
                    Documento = "78140716669",
                    DataNascimento = Convert.ToDateTime("1970-01-01"),
                    Email = "veraisadoracortereal@mtic.net.br",
                    Genero = GeneroEnum.Feminino.GetDisplayName(),
                    Status = StatusEnum.Inativo,
                    Telefone = "61994063594",
                    Tipo = TipoUsuarioEnum.Gerente
                }
          );
        }

        private static void SeedDataColecoesModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ColecaoModel>().HasData(
                new ColecaoModel
                {
                    Id = 1,
                    Nome = "Colecao Inverno",
                    AnoLancamento = 2022,
                    Estacao = EstacaoEnum.Inverno,
                    Marca = "CeA",
                    StatusSistema = StatusEnum.Ativo,
                    Orcamento = 145987.98M,
                    ResponsavelId = 3
                },
                new ColecaoModel
                {
                    Id = 2,
                    Nome = "Colecao Verao",
                    AnoLancamento = 2050,
                    Estacao = EstacaoEnum.Verao,
                    Marca = "MARISA",
                    StatusSistema = StatusEnum.Inativo,
                    Orcamento = 45398.01M,
                    ResponsavelId = 1
                });
        }

        private static void SeedDataModelosModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ModeloModel>().HasData(
               new ModeloModel
               {
                   Id = 1,
                   Nome = "Modelo A Colecao Inverno",
                   ColecaoId = 1,
                   Tipo = TipoEnum.Calca,
                   Layout = LayoutEnum.Estampa
               },
               new ModeloModel
               {
                   Id = 2,
                   Nome = "Modelo B Colecao Inverno",
                   ColecaoId = 1,
                   Tipo = TipoEnum.Camisa,
                   Layout = LayoutEnum.Bordado
               },
               new ModeloModel
               {
                   Id = 3,
                   Nome = "Modelo C Colecao Inverno",
                   ColecaoId = 1,
                   Tipo = TipoEnum.Bermuda,
                   Layout = LayoutEnum.Liso
               },
               new ModeloModel
               {
                   Id = 4,
                   Nome = "Modelo A Colecao VERAO",
                   ColecaoId = 2,
                   Tipo = TipoEnum.Bermuda,
                   Layout = LayoutEnum.Bordado
               },
               new ModeloModel
               {
                   Id = 5,
                   Nome = "Modelo B Colecao VERAO",
                   ColecaoId = 2,
                   Tipo = TipoEnum.Biquini,
                   Layout = LayoutEnum.Liso
               }
              );
        }
    }
}
