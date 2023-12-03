using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class AgregarHamburguesaIngredienteDto
    {
        //public int Hamburguesa_id { get; set; }
        //las referencias 
        public NuevoIngredienteDto Ingrediente { get; set; }
    }
}