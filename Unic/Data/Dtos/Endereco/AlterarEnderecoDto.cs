using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Unic.Data.Dtos
{
    public class AlterarEnderecoDto
    {
        [Required]
        public string Logradouro { get; set; }

        [Required]
        public string Numero { get; set; }

        [Required]
        public string Bairro { get; set; }

        [Required]
        public string Cidade { get; set; }

        [Required]
        public string Estado { get; set; }

        [Required]
        public int Cep { get; set; }

    }
}
