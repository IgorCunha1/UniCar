﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Unic.Data;

namespace Unic.Migrations
{
    [DbContext(typeof(UnicContext))]
    partial class UnicContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Unic.Models.Anos", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("codigo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("modeloID")
                        .HasColumnType("int");

                    b.Property<string>("nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Anos");
                });

            modelBuilder.Entity("Unic.Models.Carro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AnoModeloId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataCadastro")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataCompra")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataVenda")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("KM")
                        .HasColumnType("int");

                    b.Property<int>("MarcaId")
                        .HasColumnType("int");

                    b.Property<int>("ModeloId")
                        .HasColumnType("int");

                    b.Property<string>("PessoaCompradora")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PessoaVendedora")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PrecoCompra")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PrecoVenda")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("ValorTotalManutencao")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("AnoModeloId")
                        .IsUnique();

                    b.HasIndex("MarcaId")
                        .IsUnique();

                    b.HasIndex("ModeloId")
                        .IsUnique();

                    b.ToTable("Carro");
                });

            modelBuilder.Entity("Unic.Models.Endereco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Cep")
                        .HasColumnType("int");

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Endereco");
                });

            modelBuilder.Entity("Unic.Models.Manutencao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CarroId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Origem")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CarroId");

                    b.ToTable("Manutencao");
                });

            modelBuilder.Entity("Unic.Models.Marca", b =>
                {
                    b.Property<int>("codigo")
                        .HasColumnType("int");

                    b.Property<string>("nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("codigo");

                    b.ToTable("Marca");
                });

            modelBuilder.Entity("Unic.Models.Modelo", b =>
                {
                    b.Property<int>("codigo")
                        .HasColumnType("int");

                    b.Property<int>("MarcaId")
                        .HasColumnType("int");

                    b.Property<string>("nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("codigo");

                    b.ToTable("Modelo");
                });

            modelBuilder.Entity("Unic.Models.Pessoa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataCriaCao")
                        .HasColumnType("datetime2");

                    b.Property<int>("EnderecoId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Nascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("NomeCompleto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EnderecoId")
                        .IsUnique();

                    b.ToTable("Pessoa");
                });

            modelBuilder.Entity("Unic.Models.Carro", b =>
                {
                    b.HasOne("Unic.Models.Anos", "AnoModelo")
                        .WithOne("Carro")
                        .HasForeignKey("Unic.Models.Carro", "AnoModeloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Unic.Models.Marca", "Marca")
                        .WithOne("Carro")
                        .HasForeignKey("Unic.Models.Carro", "MarcaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Unic.Models.Modelo", "Modelo")
                        .WithOne("Carro")
                        .HasForeignKey("Unic.Models.Carro", "ModeloId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AnoModelo");

                    b.Navigation("Marca");

                    b.Navigation("Modelo");
                });

            modelBuilder.Entity("Unic.Models.Manutencao", b =>
                {
                    b.HasOne("Unic.Models.Carro", "Carro")
                        .WithMany("Manutencoes")
                        .HasForeignKey("CarroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Carro");
                });

            modelBuilder.Entity("Unic.Models.Pessoa", b =>
                {
                    b.HasOne("Unic.Models.Endereco", "Endereco")
                        .WithOne("Pessoa")
                        .HasForeignKey("Unic.Models.Pessoa", "EnderecoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Endereco");
                });

            modelBuilder.Entity("Unic.Models.Anos", b =>
                {
                    b.Navigation("Carro");
                });

            modelBuilder.Entity("Unic.Models.Carro", b =>
                {
                    b.Navigation("Manutencoes");
                });

            modelBuilder.Entity("Unic.Models.Endereco", b =>
                {
                    b.Navigation("Pessoa");
                });

            modelBuilder.Entity("Unic.Models.Marca", b =>
                {
                    b.Navigation("Carro");
                });

            modelBuilder.Entity("Unic.Models.Modelo", b =>
                {
                    b.Navigation("Carro");
                });
#pragma warning restore 612, 618
        }
    }
}
