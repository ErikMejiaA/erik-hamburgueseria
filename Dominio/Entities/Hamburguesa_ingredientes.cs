using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio.Entities
{
    public class Hamburguesa_ingredientes
    {
        public int Hamburguesa_id { get; set; }
        public int Ingrediente_id { get; set; }

        //las referencias 
        public Hamburguesa Hamburguesa { get; set; }
        public Ingrediente Ingrediente { get; set; }
    }
}