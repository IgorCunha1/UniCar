using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Unic.Enum.Enums;
using Unic.Data.Dtos.Manutencao;
using Unic.Repositories.Interfaces;
using System.Linq;
using AutoMapper;
using Unic.Models;

namespace Unic.Data.Dtos.Carro
{
    public class RecuperarCarroDto
    {

        public RecuperarCarroDto()
        {

        }

        [Required]
        public int Id { get; set; }

        [Required]
        public string Marca { get; set; }

        [Required]
        public string Modelo { get; set; }

        [Required]
        public int AnoFabricacao { get; set; }

        [Required]
        public int AnoModelo { get; set; }

        [Required]
        public int KM { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public decimal PrecoVenda { get; set; }

        [Required]
        public decimal PrecoCompra { get; set; }

        [Required]
        public string PessoaCompradora { get; set; }

        [Required]
        public string PessoaVendedora { get; set; }

        [Required]
        public StatusCarro Status { get; set; }

        [Required]
        public string Placa { get; set; }

        public decimal ValorTotalManutencao { get; set; }

        public virtual List<Unic.Models.Manutencao> Manutencoes { get; set; }
    }
}
