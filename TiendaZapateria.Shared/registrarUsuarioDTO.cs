using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaZapateria.Shared
{
    public class registrarUsuarioDTO
    {
        public int IdUsuario { get; set; }

        public int IdRol { get; set; }

        public string PrimerNombre { get; set; } = null!;

        public string SegundoNombre { get; set; } = null!;

        public string PrimerApellido { get; set; } = null!;

        public string SegundoApellido { get; set; } = null!;

        public string? Email { get; set; }

        public string NombreUsuario { get; set; } = null!;

        public string ClaveAcceso { get; set; } = null!;

        public int? Telefono { get; set; }

        public bool? Estado { get; set; }

        
    }
}
