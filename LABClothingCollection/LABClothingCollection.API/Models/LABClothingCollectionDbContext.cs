using LABClothingCollection.API.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;

namespace LABClothingCollection.API.Models
{
    /// <summary>
    /// Iteração com o banco de dados, nas tabelas utilizando o DBSet
    /// </summary>
    public class LABClothingCollectionDbContext : DbContext //Herança - LABClothingCollectionDbContext é uma subclasse de DbContext
    {
        //construtor da classe, Recibe parametro options do tipo DbContextOptions.
        //chama o construtor base da classe DbContext, que recebe as opções de configuração necessárias para estabelecer a conexão com a base de dados.
        public LABClothingCollectionDbContext(DbContextOptions options) : base(options)
        {
        }

        //configura a estrutura da base de dados e suas relações.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ColecaoModel>().HasOne(e => e.Responsavel)  //relação entre as entidades ColecaoModel e UsuarioModel. HasOne= um-pra-muitos
                        .WithMany(x => x.Colecao)
                        .Metadata
                        .DeleteBehavior = DeleteBehavior.Restrict; //restringe a eliminação de Usuario para evitar conflitos de integridade referencial.

            modelBuilder.Entity<ModeloModel>().HasOne(e => e.Colecao)
                        .WithMany(x => x.Modelos)
                        .Metadata
                        .DeleteBehavior = DeleteBehavior.Restrict;

            //métodos privados chamados para dar carga de dados iniciais às tabelas da base de dados. 
            SeedDataUsuariosModel(modelBuilder);
            SeedDataColecoesModel(modelBuilder);
            SeedDataModelosModel(modelBuilder);
        }

        public virtual DbSet<UsuarioModel> Usuarios { get; set; } //propriedade q representa uma coleção de entidades UsuarioModel. Cada entidade UsuarioModel corresponderá a um registro na tabela "Usuário" da base de dados.
        public virtual DbSet<ColecaoModel> Colecoes { get; set; } //propriedade q representa uma coleção de entidades ColecaoModel. Cada entidade ColecaoModel corresponderá a um registro na tabela "Colecao" da base de dados.
        public virtual DbSet<ModeloModel> Modelos { get; set; } //propriedade q representa uma coleção de entidades ModeloModel. Cada entidade ModeloModel corresponderá a um registro na tabela "Modelo" da base de dados.

        //Cargas de dados iniciais nas respectivas tabelas.

        private static void SeedDataUsuariosModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioModel>().HasData(
              new UsuarioModel
              {
                  Id = 1,
                  NomeCompleto = "Mariana De Carvalho Gonçaves Daruix",
                  Documento = "23188028075",
                  DataNascimento = Convert.ToDateTime("1990-06-26"),
                  Email = "marianadcgd@hotmail.com",
                  Genero = GeneroEnum.Feminino.GetDisplayName(),
                  Status = StatusEnum.Ativo,
                  Telefone = "11996448176",
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
                  Telefone = "11995624145",
                  Tipo = TipoUsuarioEnum.Criador
              },
              new UsuarioModel
              {
                  Id = 3,
                  NomeCompleto = "Isabella Marina Nunes",
                  Documento = "83934472000149",
                  DataNascimento = Convert.ToDateTime("2001-01-07"),
                  Email = "isabella_marina_nunes@uol.com.br",
                  Genero = GeneroEnum.Feminino.GetDisplayName(),
                  Status = StatusEnum.Ativo,
                  Telefone = "11991551385",
                  Tipo = TipoUsuarioEnum.Gerente
              },
              new UsuarioModel
              {
                  Id = 4,
                  NomeCompleto = "Aline Brenda Freitas",
                  Documento = "81994451300",
                  DataNascimento = Convert.ToDateTime("1984-02-10"),
                  Email = "alinebrendafreitas@yahoo.com",
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
                    DataNascimento = Convert.ToDateTime("1970-01-11"),
                    Email = "veraisadoracortereal@mtic.net.br",
                    Genero = GeneroEnum.Feminino.GetDisplayName(),
                    Status = StatusEnum.Inativo,
                    Telefone = "11994063594",
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
//DbContext representa o contexto da base de dados do aplicativo
//é utilizada para configurar as entidades e suas relações na base de dados.
//Além disso, contém propriedades para acessar as coleções de entidades UsuarioModel, ColecaoModely ModeloModel,
//que se mapearão para as tabelas "Usuário", "Coleção" e "Modelo" na base de dados, respectivamente.
