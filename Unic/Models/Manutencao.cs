using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Unic.Models
{
    public class Manutencao
    {
        public int Id { get; set; }

        public DateTime DataCriacao { get; set; }

        public string Origem { get; set; }

        public string Descricao { get; set; }

        public decimal Valor { get; set; }

        public int CarroId { get; set; }

        [JsonIgnore]
        public virtual Carro Carro { get; set; }
    }
}
