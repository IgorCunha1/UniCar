using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Unic.Models
{
    public class Anos
    {
        public Anos() { }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string codigo { get; set; }
        public string nome { get; set; }

    }
}
