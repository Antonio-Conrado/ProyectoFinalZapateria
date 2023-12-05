using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaZapateria.Shared
{
    public class SesionDTO
    {
        public int IdRol { get; set; }
        public int IdUsuario { get; set; }


        public string Email { get; set; } = null!;

        public string NombreUsuario { get; set; } = null!;
        public string NombreRol { get; set; } = null!;
    }
}
