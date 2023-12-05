using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaZapateria.Shared
{
    public class RegistrarDetalleInventarioDTO
    {

        public int IdCategoria { get; set; }

        public int IdMarca { get; set; }

        public int IdColor { get; set; }

        public int IdTalla { get; set; }

        public int IdMaterial { get; set; }



        public virtual RegistrarProductoDTO IdProductoNavigation { get; set; } = null!;
    }
}
