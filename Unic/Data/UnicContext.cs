﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unic.Models;

namespace Unic.Data
{
    public class UnicContext : DbContext
    {
        public UnicContext(DbContextOptions<UnicContext> options) : base(options)
        {

        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Endereco>()
                .HasOne(Endereco => Endereco.Pessoa)
                .WithOne(Pessoa => Pessoa.Endereco)
                .HasForeignKey<Pessoa>(Pessoa => Pessoa.EnderecoId);

            builder.Entity<Carro>()
                .HasMany(Carro => Carro.Manutencoes)
                .WithOne(Manutencao => Manutencao.Carro)
                .HasForeignKey(Manutencao => Manutencao.CarroId);

            builder.Entity<Marca>()
                .HasMany(Marca => Marca.Carros)
                .WithOne(Carro => Carro.Marca);
                
            builder.Entity<Modelo>()
                .HasMany(Modelo => Modelo.Carros)
                .WithOne(Carro => Carro.Modelo);

            builder.Entity<Anos>()
                .HasMany(Anos => Anos.Carros)
                .WithOne(Carro => Carro.AnoModelo);
                

        }

        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Endereco> Endereco { get; set; }
        public DbSet<Carro> Carro { get; set; }
        public DbSet<Manutencao> Manutencao { get; set; }
        public DbSet<Marca> Marca { get; set; }
        public DbSet<Modelo> Modelo { get; set; }
        public DbSet<Anos> Anos { get; set; }




    }
}
