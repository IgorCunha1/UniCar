using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Unic.Models
{
    public class Marca
    {
        public Marca() { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int codigo { get; set; }
        public string nome { get; set; }

        [JsonIgnore]
        public virtual Carro Carro { get; set; }

    }
}
