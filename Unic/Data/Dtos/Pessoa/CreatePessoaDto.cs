using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Unic.Data.Dtos.Pessoa
{
    public class CreatePessoaDto
    {
        [Required]
        public string NomeCompleto { get; set; }

        [Required]
        public string Cpf { get; set; }

        public DateTime Nascimento { get; set; }

        [Required]
        public DateTime DataCriaCao { get; set; }

        [Required]
        public int EnderecoID { get; set; }
    }
}
