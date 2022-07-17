using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Unic.Models
{

    public class Pessoa 
    {
        
        public Pessoa(){  }

        [Key]
        [Required]
        public int Id { get; set; }
       
        [Required]
        public string NomeCompleto { get; set; }

        [Required]
        public string Cpf { get; set; }
        
        [Required]
        public DateTime Nascimento { get; set; }

        [Required]
        public DateTime DataCriaCao { get; set; }

        public int EnderecoId { get; set; }

        public virtual Endereco Endereco { get; set; }
    }
}
