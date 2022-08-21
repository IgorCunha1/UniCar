using AutoMapper.Configuration.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Unic.Enum;
using static Unic.Enum.Enums;

namespace Unic.Models
{

    public class Carro 
    {
        
        public Carro(){  }

        [Key]
        [Required]
        public int Id { get; set; }
       
        [Required]
        public string Marca { get; set; }

        [Required]
        public string Modelo { get; set; }

        [Required]
        public int AnoModelo { get; set; }

        [Required]
        public int KM { get; set; }

        [Required]
        public string Placa { get; set; }

        [Required]
        public string Descricao { get; set; }

        
        public decimal PrecoVenda { get; set; }

        
        public decimal PrecoCompra { get; set; }

        
        public string PessoaCompradora { get; set; }

        
        public string PessoaVendedora { get; set; }

        [Required]
        public StatusCarro Status { get; set; }

        
        public DateTime DataCadastro { get; set; }

        
        public DateTime DataCompra { get; set; }

      
        public DateTime DataVenda { get; set; }

        public virtual List<Manutencao> Manutencoes { get; set; }

        
        public decimal ValorTotalManutencao { get; set; }

    }
}
