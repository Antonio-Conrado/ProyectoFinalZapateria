using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaZapateria.Shared
{
    public class VistaDTO
    {
        public int IdVista { get; set; }

        public int IdRol { get; set; }

        public string? UrlName { get; set; }

        public bool? Estado { get; set; }

        //public virtual TblRol IdRolNavigation { get; set; } = null!;
    }
}
