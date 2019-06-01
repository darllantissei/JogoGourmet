using JogoGourmet.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoGourmet.Class
{
    public class PratosGourmet : IPratosGourmet
    {
        public string NomePrato { get; set; }
        public string TipoPrato { get; set; }
    }
}
