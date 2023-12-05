using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaZapateria.Shared
{
    public class MarcaDTO
    {
        public int IdMarca { get; set; }

        [Required(ErrorMessage = "Ingrese la marca")]
        public string NombreMarca { get; set; } = null!;
    }
}
