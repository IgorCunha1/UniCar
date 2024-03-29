﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Unic.Enum.Enums;

namespace Unic.Data.Dtos.Carro
{
    public class CreateCarroDto
    {
        [Required]
        public string MarcaId { get; set; }

        [Required]
        public string ModeloId { get; set; }

        [Required]
        public string AnoModeloId { get; set; }

        [Required]
        public int KM { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public string PrecoCompra { get; set; }

        [Required]
        public string PessoaVendedora { get; set; }

        [Required]
        public string Placa { get; set; }

        public string Status { get; set; }
    }
}
