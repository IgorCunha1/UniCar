using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Unic.Models
{
    public class Anos
    {
        public Anos() { }

        [Key]
        public int id { get; set; }

        public string codigo { get; set; }
        public string nome { get; set; }
        public int modeloID { get; set; }

        [JsonIgnore]
        public virtual Carro Carro { get; set; }
    }
}
