using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaZapateria.Shared
{
    public class TallaDTO
    {
        public int IdTalla { get; set; }

        [Required(ErrorMessage = "Ingrese la talla")]
        public double? TallaEuropea { get; set; }
    }
}
