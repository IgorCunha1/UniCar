using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Unic.Models;

namespace Unic.Data.Dtos.Pessoa
{
    public class RecuperarPessoaDto
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string NomeCompleto { get; set; }

        [Required]
        public string Cpf { get; set; }

        [Required]
        public DateTime Nascimento { get; set; }

        [Required]
        public Endereco Endereco { get; set; }
    }
}
