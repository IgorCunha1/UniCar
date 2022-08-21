using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unic.Models;

namespace Unic.Data.Dtos.ModeloDto
{
    public class ModeloAnoDto
    {
        public List<APIModeloDto> modelos { get; set; }
        public List<Anos> anos { get; set; }
    }
}
