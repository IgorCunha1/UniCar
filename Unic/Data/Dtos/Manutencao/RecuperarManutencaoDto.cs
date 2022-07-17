using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Unic.Data.Dtos.Manutencao
{
    public class RecuperarManutencaoDto
    {
        public string Origem { get; set; }

        public string Descricao { get; set; }

        public decimal Valor { get; set; }

        public int CarroId { get; set; }

        public Unic.Models.Carro Carro { get; set; }
    }
}
