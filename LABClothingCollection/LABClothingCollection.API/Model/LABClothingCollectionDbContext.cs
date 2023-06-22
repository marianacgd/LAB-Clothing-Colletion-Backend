using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace LABClothingCollection.API.Model
{
    /// <summary>
    /// Iteração com o banco de dados, nas tabelas utilizando o DBSet
    /// </summary>
    public class LABClothingCollectionDbContext : DbContext
    {
        public LABClothingCollectionDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
