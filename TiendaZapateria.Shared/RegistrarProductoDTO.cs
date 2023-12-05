using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaZapateria.Shared
{
    public class RegistrarProductoDTO
    {
        public string NombreProducto { get; set; } = null!;

        public double PrecioVenta { get; set; }
    }
}
