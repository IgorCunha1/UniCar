﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Unic.Models;
using static Unic.Enum.Enums;

namespace Unic.Data.Dtos.Carro
{
    public class ListarCarroDto
    {

        [Required]
        public string Descricao { get; set; }

        [Required]
        public decimal PrecoVenda { get; set; }

        [Required]
        public StatusCarro Status { get; set; }

        [Required]
        public string Placa { get; set; }

        [Required]
        public virtual List<Unic.Models.Manutencao> Manutencoes{ get; set; }

        [Required]
        public virtual Marca Marca { get; set; }

        [Required]
        public virtual Modelo Modelo { get; set; }

        [Required]
        public virtual Anos AnoModelo { get; set; }

    }
}
