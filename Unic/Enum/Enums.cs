using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Unic.Enum
{
    public class Enums
    {
        public enum StatusCarro
        {
            [Description("Disponivel")]
            Disponivel = 1,
            [Description("Vendido")]
            Vendido = 2,
            [Description("Negociacao")]
            Negociacao = 3
        }
    }
}
