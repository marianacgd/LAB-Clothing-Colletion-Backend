﻿using LABClothingCollection.API.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public virtual DbSet<UsuarioModel> Usuarios { get; set; }
        public virtual DbSet<ColecaoModel> Colecoes { get; set; }

    }
}
